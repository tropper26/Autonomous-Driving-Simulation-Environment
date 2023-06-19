
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace scripts
{
    public class RoadManager : MonoBehaviour
    {

        private static RoadManager instance;
        public static RoadManager Instance
        {
            get
            {
                return instance;
            }
        }

        // List of all roads
        public List<RoadPiece> roadList = new();

        // selected Road
        public RoadPiece selectedRoad = null;

        // selected Object, not necessarily a road
        GameObject selectedObject;

        // boolean for road validation. If false, certain functions are disabled
        public bool inValidPosition = true;

        // used to check whether the user is currently dragging. So no other object can be selected during a drag. 
        private bool isDragging = false;

        private bool isInStretchArea = false;
        private bool isStretching = false;
        private bool isSnapped = false;

        // Rotating Angle of the Pieces
        private const float ROTATING_ANGLE = 15f;

        // Maximum Snapping Distance between Objects
        private const float MAX_SNAPPING_DISTANCE = 200;
        private const float MAX_SNAPPING_AREA = 140 * 2 + MAX_SNAPPING_DISTANCE;

        private VirtualAnchor stretchingAnchor;

        private float stretchingDistance = 3.78f * 5;


        /*
         * Sets the instance, so other classes can refer
         */
        private void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            if (selectedRoad != null)
            {
                CheckStretchPosition();
            }
            // This condition checks, whether a tile is being dragged. 
            if (Input.GetMouseButton(0))
            {
                if (!isDragging && isInStretchArea)
                {
                    StretchRoad();
                }
                else
                {
                    DragAndDropRoad();
                }
            }

            // This condition checks, when the user releases the drag and checks the road position
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                if (isStretching)
                {
                    GroupToParentRoad();
                }
                isStretching = false;
                stretchingDistance = 3.78f * 5;
                validateRoadPosition();
            }

            // These conditions checks, whether the user wants to rotate the piece clockwise or counter-clockwise
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isSnapped)
                {
                    int currentIndex = selectedRoad.anchorPoints.FindIndex(anchor => anchor == selectedRoad.lastSelectedSnappedAnchorPoint);
                    VirtualAnchor nextAnchor = selectedRoad.anchorPoints[(currentIndex + 1) % selectedRoad.anchorPoints.Count];
                    CompareAnchorPointOrientation(selectedRoad.lastNeighborSnappedAnchorPoint, nextAnchor);
                    foreach (VirtualAnchor va in selectedRoad.anchorPoints)
                    {
                        va.RemoveConntectedAnchorPoint();
                    }
                    //selectedRoad.lastSelectedSnappedAnchorPoint.RemoveConntectedAnchorPoint();
                    selectedRoad.lastSelectedSnappedAnchorPoint = nextAnchor;
                    Snap();
                }
                else
                {
                    rotateRoadPiece(-ROTATING_ANGLE);
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isSnapped)
                {
                    int currentIndex = selectedRoad.anchorPoints.FindIndex(anchor => anchor == selectedRoad.lastSelectedSnappedAnchorPoint);
                    if (currentIndex == 0)
                    {
                        currentIndex = selectedRoad.anchorPoints.Count;
                    }
                    VirtualAnchor nextAnchor = selectedRoad.anchorPoints[(currentIndex - 1)];
                    CompareAnchorPointOrientation(selectedRoad.lastNeighborSnappedAnchorPoint, nextAnchor);
                    foreach (VirtualAnchor va in selectedRoad.anchorPoints)
                    {
                        va.RemoveConntectedAnchorPoint();
                    }
                    //selectedRoad.lastSelectedSnappedAnchorPoint.RemoveConntectedAnchorPoint();
                    selectedRoad.lastSelectedSnappedAnchorPoint = nextAnchor;
                    Snap();
                }
                else
                {
                    rotateRoadPiece(ROTATING_ANGLE);
                }
            }

            // This condition checks, whether the user wants to lock a road piece. This can only be applied, when a road is selected. 
            if (Input.GetKeyDown(KeyCode.L) && selectedRoad != null)
            {
                selectedRoad.setIsLocked(!selectedRoad.getIsLocked());
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                DeleteRoad(this.selectedRoad);
            }

            // This condition checks, whether the user wants to deselect the road he has clicked. 
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeselectRoad();
            }
        }
        public void CheckStretchPosition()
        {
            GameObject obj = GetMouseObject();

            if (obj != null && obj.GetComponent<RoadPiece>() != null)
            {
                RoadPiece road = obj.GetComponent<RoadPiece>();

                foreach (VirtualAnchor anchor in selectedRoad.anchorPoints)
                {
                    if (anchor.connectedAnchorPoint == null)
                    {
                        if (!isStretching)
                        {

                            if (Vector3.Distance(GetWorldPositionFromMouse(), road.transform.position + anchor.offset) < 50)
                            {
                                //UnityEngine.Cursor.SetCursor(cursorStretchTexture, Vector2.zero, CursorMode.Auto);
                                colorRoadPiece(SelectionColor.invalid);
                                isInStretchArea = true;
                                stretchingAnchor = anchor;
                                return;
                            }
                            else
                            {
                                colorRoadPiece(SelectionColor.selected);
                                isInStretchArea = false;
                                stretchingAnchor = null;
                                //UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                            }
                        }
                    }
                }

            }
            else if (!isStretching)
            {
                colorRoadPiece(SelectionColor.selected);
                isInStretchArea = false;
                stretchingAnchor = null;
                //UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }

        public void GroupToParentRoad()
        {

        }
        public void StretchRoad()
        {


            isStretching = true;
            Vector3 origin = selectedRoad.transform.position + stretchingAnchor.offset;

            //Normalize Offset Vector
            Vector3 offsetNormalized = stretchingAnchor.offset.normalized;

            //Get Mouse Position
            Vector3 mousePosition = GetWorldPositionFromMouse();

            //Get the Vector from the mouse to the anchor point
            Vector3 vectorToMouse = mousePosition - origin;

            //calculate the dot Product to check, that the distance is positive in the direction of the vector. (So moving mouse in opposite direction will result in negative value)
            float dotProduct = Vector3.Dot(offsetNormalized, vectorToMouse);

            //Create Vector between Anchor Point and Mouse, but only the direction of the offset
            Vector3 projectedVector = Vector3.Project(vectorToMouse, offsetNormalized);

            // Calculate the distance of that vector which only checks the distance in offset direction. 
            float distance = projectedVector.magnitude;

            if (dotProduct >= 0 && distance >= stretchingDistance)
            {
                Vector3 newPosition = origin + (offsetNormalized * stretchingDistance) - (offsetNormalized * (3.78f * 5 / 2));

                RoadPiece roadPiece = PrefabManager.Instance.GetPieceOfType(RoadType.StraightShort);
                RoadPiece straight = Instantiate(roadPiece, newPosition, Quaternion.Euler(0f, 0f, stretchingAnchor.orientation));
                straight.transform.SetParent(stretchingAnchor.referencedRoadPiece.transform);
                stretchingAnchor.AddStretchingStraight(straight);

                stretchingDistance += 3.78f * 5;
            }
            else if (dotProduct < stretchingDistance - 3.78f * 5)
            {
                if (stretchingAnchor.GetStretchingStraights().Count > 0)
                {
                    RoadPiece straight = stretchingAnchor.GetStretchingStraights()[stretchingAnchor.GetStretchingStraights().Count - 1];
                    DeleteRoad(straight);
                    stretchingAnchor.RemoveLastStretchingStraight();

                    stretchingDistance -= 3.78f * 5;
                }
            }

        }

        /*
         * This method will add every created road piece to the list of roads. This way, snapping can be implemented because the positions of roads can be determined. 
         */
        public void AddRoadToList(RoadPiece road)
        {
            roadList.Add(road);
        }

        /*
         * Removes a road from the Roads List
         */
        public void RemoveRoadFromList(RoadPiece road)
        {
            roadList.Remove(road);
        }
        /* 
         * This method creates a new road piece. It gets the selected road type from the sidebar and creates the corresponding prefab on the screen. 
         */
        public void createRoad()
        {
            // Creates the new roadpiece
            var roadPiece = PrefabManager.Instance.GetPieceOfType(ButtonManager.Instance.getSelectedRoadType());
            var newRoadPiece = Instantiate(roadPiece, GetWorldPositionFromMouse(), Quaternion.identity);

            // Sets the valid position to false (As it will always spawn on the sidebar), adds the road to the list of roads and selects the road automatically upon creation, allowing the user to instantly drag it)
            inValidPosition = false;
            SelectRoad(newRoadPiece);
        }

        /*
         * This method deletes the selected road
         */
        public void DeleteRoad(RoadPiece road)
        {

            if (road != null && !road.getIsLocked())
            {
                // We have to destroy the selectedObject, as the road is not a GameObject, but a RoadPiece. The effect will be the same, though. 

                foreach (VirtualAnchor va in road.anchorPoints)
                {
                    va.RemoveConntectedAnchorPoint();
                }
                RemoveRoadFromList(road);
                if (road == selectedRoad)
                {
                    DeselectRoad();
                    foreach (VirtualAnchor va in road.anchorPoints)
                    {
                        foreach (RoadPiece stretchingRoad in va.GetStretchingStraights())
                        {
                            DeleteRoad(stretchingRoad);
                        }
                    }
                }
                Destroy(road.gameObject);
            }
        }

        /*
         * This method drags a road across the screen. When the user drags a piece, it will follow the cursor of the mouse. This is only the case, if the road piece is not locked
         */
        private void DragAndDropRoad()
        {
            if (!isStretching)
            {
                // This will only check the object at the start of dragging and not check every frame. Prevents the user from going too fast and "Losing" the road. 
                if (isDragging == false)
                {
                    selectedObject = GetMouseObject();
                }

                // This will, if a road is selected, set dragging to true, deselect previous roads and select the new road. The road is then dragged with the mouse
                if (selectedObject != null)
                {
                    if (selectedObject.GetComponent<RoadPiece>() != null)
                    {
                        DeselectRoad();
                        SelectRoad(selectedObject.GetComponent<RoadPiece>());

                        if (!selectedRoad.getIsLocked())
                        {
                            isDragging = true;
                            isSnapped = false;
                            selectedRoad.transform.position = GetWorldPositionFromMouse();
                            foreach (VirtualAnchor va in selectedRoad.anchorPoints)
                            {
                                va.RemoveConntectedAnchorPoint();
                            }

                            if (!Input.GetKey(KeyCode.LeftShift))
                            {
                                Snap();
                            }
                            validateRoadPosition();
                        }

                    }
                }
            }
        }


        public List<RoadPiece> GetNearestNeighborsInArea()
        {
            List<RoadPiece> roadsInArea = new List<RoadPiece>();
            if (roadList.Count > 1)
            {
                foreach (RoadPiece road in roadList)
                {
                    if (Vector3.Distance(selectedRoad.transform.position, road.transform.position) <= MAX_SNAPPING_AREA && selectedRoad != road)
                    {
                        roadsInArea.Add(road);
                    }
                }
            }
            return roadsInArea;
        }

        public (VirtualAnchor selectedRoadVA, VirtualAnchor nearestNeighborVA) GetNearestAnchorPoints(List<RoadPiece> nearestNeighbors)
        {
            float nearestDistance = -1;
            VirtualAnchor selectedRoadVA = null;
            VirtualAnchor nearestNeighborVA = null;

            foreach (RoadPiece roadPiece in nearestNeighbors)
            {
                foreach (VirtualAnchor neighborVA in roadPiece.anchorPoints)
                {
                    if (nearestDistance == -1 || Vector3.Distance(neighborVA.referencedRoadPiece.transform.position + neighborVA.offset, selectedRoad.transform.position) < nearestDistance)
                    {
                        nearestDistance = Vector3.Distance(neighborVA.referencedRoadPiece.transform.position + neighborVA.offset, selectedRoad.transform.position);
                        nearestNeighborVA = neighborVA;
                    }
                }
            }
            float nearestAnchorPoint = -1;
            if (nearestNeighborVA != null)
            {
                foreach (VirtualAnchor selectedVA in selectedRoad.anchorPoints)
                {
                    if (nearestNeighborVA.connectedAnchorPoint == null && (nearestAnchorPoint == -1 || Vector3.Distance(nearestNeighborVA.referencedRoadPiece.transform.position + nearestNeighborVA.offset, selectedVA.referencedRoadPiece.transform.position + selectedVA.offset) < nearestAnchorPoint))
                    {
                        nearestAnchorPoint = Vector3.Distance(nearestNeighborVA.referencedRoadPiece.transform.position + nearestNeighborVA.offset, selectedVA.referencedRoadPiece.transform.position + selectedVA.offset);
                        selectedRoadVA = selectedVA;
                    }
                }
            }
            return (selectedRoadVA, nearestNeighborVA);
        }

        public void CompareAnchorPointOrientation(VirtualAnchor neighbor, VirtualAnchor selected)
        {
            if (Mathf.Abs(neighbor.orientation - selected.orientation) == 180)
            {
                return;
            }
            else
            {
                float orientationDifference = Mathf.Abs(neighbor.orientation - selected.orientation);
                Debug.Log(orientationDifference);
                float neededOrientation = orientationDifference - 180;

                if (neighbor.orientation >= selected.orientation)
                {

                    rotateRoadPiece(neededOrientation);
                }
                else
                {
                    rotateRoadPiece(-neededOrientation);
                }

            }
        }

        public void Snap()
        {
            List<RoadPiece> nearestNeighbors = GetNearestNeighborsInArea();
            var nearestAnchorPoints = GetNearestAnchorPoints(nearestNeighbors);
            if (nearestAnchorPoints.nearestNeighborVA != null && nearestAnchorPoints.selectedRoadVA != null)
            {
                if (selectedRoad.lastNeighborSnappedAnchorPoint != nearestAnchorPoints.nearestNeighborVA)
                {
                    CompareAnchorPointOrientation(nearestAnchorPoints.nearestNeighborVA, nearestAnchorPoints.selectedRoadVA);
                    selectedRoad.transform.position = (nearestAnchorPoints.nearestNeighborVA.referencedRoadPiece.transform.position + nearestAnchorPoints.nearestNeighborVA.offset) - nearestAnchorPoints.selectedRoadVA.offset;
                    nearestAnchorPoints.selectedRoadVA.ConnectAnchorPoint(nearestAnchorPoints.nearestNeighborVA);
                    selectedRoad.lastNeighborSnappedAnchorPoint = nearestAnchorPoints.nearestNeighborVA;
                    selectedRoad.lastSelectedSnappedAnchorPoint = nearestAnchorPoints.selectedRoadVA;
                }
                else
                {
                    CompareAnchorPointOrientation(nearestAnchorPoints.nearestNeighborVA, selectedRoad.lastSelectedSnappedAnchorPoint);
                    selectedRoad.transform.position = (nearestAnchorPoints.nearestNeighborVA.referencedRoadPiece.transform.position + nearestAnchorPoints.nearestNeighborVA.offset) - selectedRoad.lastSelectedSnappedAnchorPoint.offset;
                    selectedRoad.lastSelectedSnappedAnchorPoint.ConnectAnchorPoint(nearestAnchorPoints.nearestNeighborVA);
                }
                this.isSnapped = true;
                getNeighborsReferences(nearestNeighbors);
            }
        }

        /*
         * This sets the selected road and will change the color of the piece accordingly. Also, it checks the current Road Position. 
         */
        public void SelectRoad(RoadPiece road)
        {
            if (selectedRoad == null)
            {
                selectedRoad = road;
                validateRoadPosition();
                colorRoadPiece(selectedRoad.getIsLocked() ? SelectionColor.lockedSelected : SelectionColor.selected);
            }
        }

        /*
         * This deselect any road currently selected and changes the color accordingly. 
         */
        public void DeselectRoad()
        {
            if (selectedRoad != null)
            {
                colorRoadPiece(selectedRoad.getIsLocked() ? SelectionColor.locked : SelectionColor.normal);
                selectedRoad = null;
                isDragging = false;
            }
        }

        /*
         * This rotates the selected roadpiece by a given rotation. Only if the piece is not locked
         */
        private void rotateRoadPiece(float rotation)
        {
            if (selectedRoad != null && !selectedRoad.getIsLocked())
            {
                selectedRoad.transform.Rotate(new Vector3(0, 0, rotation));
                selectedRoad.setRotation(selectedRoad.getRotation() + rotation);

                foreach (VirtualAnchor va in selectedRoad.anchorPoints)
                {
                    va.Update(rotation);
                }
            }
        }

        /*
         * Connects by Reference all AnchorPoints of a Piece placed between multiple Pieces
         */
        public void getNeighborsReferences(List<RoadPiece> neighborRoads)
        {
            bool stop = false;
            foreach (VirtualAnchor vaS in selectedRoad.anchorPoints)
            {
                if (vaS.connectedAnchorPoint == null)
                {
                    foreach (RoadPiece neighbor in neighborRoads)
                    {
                        foreach (VirtualAnchor vaN in neighbor.anchorPoints)
                        {
                            if (vaN.connectedAnchorPoint == null && Vector3.Distance(vaS.referencedRoadPiece.transform.position + vaS.offset, vaN.referencedRoadPiece.transform.position + vaN.offset) < 1)
                            {
                                //Debug.Log("Distance " + Vector3.Distance(vaS.referencedRoadPiece.transform.position + vaS.offset, vaN.referencedRoadPiece.transform.position + vaN.offset));
                                vaS.ConnectAnchorPoint(vaN);
                                stop = true;
                                break;
                            }
                        }
                        if (stop == true)
                            break;
                    }
                }
                stop = false;
            }
        }


        /*
         * This will get the clicked object on the screen. Currently only works for GameObjects. 
         */
        private GameObject GetMouseObject()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                return hit.collider.gameObject;
            }

            return null;
        }

        /*
         * This translates px into world position, so the user can only click inside the camera frame
         */
        private Vector3 GetWorldPositionFromMouse()
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            return worldPosition;
        }

        /*
         * This method validates the road position. Currently always true, as not implemented yet. 
         */
        private void validateRoadPosition()
        {
            inValidPosition = true;
        }

        /*
         * This will color the road piece dependend on the status the road currently has. 
         */
        public void colorRoadPiece(SelectionColor sColor)
        {
            Color color = new Color();
            switch (sColor)
            {
                case SelectionColor.normal:
                    color = Color.white;
                    break;
                case SelectionColor.selected:
                    color = new Color(0.49f, 0.85f, 1f, 1f);
                    break;
                case SelectionColor.invalid:
                    color = new Color(1f, 0.67f, 0.72f, 1f);
                    break;
                case SelectionColor.snapped:
                    color = new Color(0.72f, 1f, 0.65f, 1f);
                    break;
                case SelectionColor.locked:
                    color = new Color(1f, 1f, 1f, 0.5f);
                    break;
                case SelectionColor.lockedSelected:
                    color = new Color(0f, 0.707f, 1f, 1f);
                    break;
                case SelectionColor.lockedSnapped:
                    color = new Color(0f, 0.7f, 0f, 1f);
                    break;
                default:
                    break;
            }
            selectedRoad.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
