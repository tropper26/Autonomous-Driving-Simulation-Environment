using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Enums;

namespace scripts
{
    /// <summary>
    /// 
    /// </summary>
    public class SidebarButtonHandler : MonoBehaviour, IPointerDownHandler
    {
        public Button SidebarButton;

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            //Every button gets added to the list of sidebar buttons at the start
            ButtonManager.Instance.AddSidebarButton(SidebarButton);
        }

        /*
         * This method reacts to a mouse button down event on a button. 
         * This is necessary, because we want to click, hold and instantly drag a new road. 
         * With a "onClickEvent" we would have to perform an entire click.
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"> </param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (RoadManager.Instance.InValidPosition && eventData.button == PointerEventData.InputButton.Left)
            {
                SelectRoadType(gameObject.name);
            }
        }

        /*
         * This method sets the selected road type according to the button pressed
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buttonName"> </param>
        void SelectRoadType(string buttonName)
        {
            RoadManager.Instance.DeselectRoad();
            var selectedRoadType = RoadType.None;
            switch (buttonName)
            {
                case "Straight":
                    selectedRoadType = RoadType.StraightRoad;
                    break;
                case "Turn":
                    selectedRoadType = RoadType.Turn;
                    break;
                case "Turn15":
                    selectedRoadType = RoadType.Turn15;
                    break;
                case "ThreeWayIntersection":
                    selectedRoadType = RoadType.ThreeWayIntersection;
                    break;
                case "FourWayIntersection":
                    selectedRoadType = RoadType.FourWayIntersection;
                    break;
                case "ParkingBottom":
                    selectedRoadType = RoadType.ParkingBottom;
                    break;
                case "ParkingTop":
                    selectedRoadType = RoadType.ParkingTop;
                    break;
                case "ParkingTopBottom":
                    selectedRoadType = RoadType.ParkingTopAndBottom;
                    break;
                case "Crosswalk":
                    selectedRoadType = RoadType.Crosswalk;
                    break;
                case "FourWayRoundAbout":
                    selectedRoadType = RoadType.FourWayRoundAbout;
                    break;
                case "ThreeWayRoundAbout":
                    selectedRoadType = RoadType.ThreeWayRoundAbout;
                    break;
                default:
                    break;
            }
            ButtonManager.Instance.HandleButtonClick(SidebarButton, selectedRoadType);
        }
    }
}