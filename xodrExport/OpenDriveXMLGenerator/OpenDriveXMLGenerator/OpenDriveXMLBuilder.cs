﻿using System.Xml;

namespace OpenDriveXMLGenerator
{
    public class OpenDriveXMLBuilder
    {
        public XmlDocument Document { get; set; }
        public XmlElement RootElement { get; set; }

        private int id = 0;
        private float x;
        private float y;

        public OpenDriveXMLBuilder()
        {
            Document = new XmlDocument();
            RootElement = Document.CreateElement("OpenDRIVE");
            Document.AppendChild(RootElement);
        }

        public XODRRoad AddStraightRoad(float startX = 0 , float startY = 0,float length = 0.0f, bool crossing = false){      

            var road = RootElement.AddRoadElement(
                name: "Road " + id.ToString(), 
                length: length.ToString(),
                id: id.ToString(),
                junction: "-1");

            id++;
                var plainView = road.AddPlainViewElement();
                    //TODO modify x and y based on previous road
                    var geometry1 = plainView.AddGeometryElement(
                        s: "0.0",
                        x: startX.ToString(),
                        y: startY.ToString(),
                        hdg: "0.0000000000000000e+0",
                        length: length.ToString());
            
            var lanes = road.AddLanesElement();
                var laneSection = lanes.AddLaneSectionElement(s:"0.0000000000000000e+000");
                    var left = laneSection.AddDirectionElement(Direction.Left);
                        var lane = left.AddLaneElement(id:"1", type:"driving", level:"false");
                            lane.AddLinkElement();
                            lane.AddWidthElement();
                    var center = laneSection.AddDirectionElement(Direction.Center);
                        var laneCenter = center.AddLaneElement(id:"0", type:"none", level:"false");
                            laneCenter.AddLinkElement();
                            laneCenter.AddWidthElement(a:"0.0000000000000000e+000");
                    var right = laneSection.AddDirectionElement(Direction.Right);
                        var laneRight = right.AddLaneElement(id:"-1", type:"driving", level:"false");
                            laneRight.AddLinkElement();
                            laneRight.AddWidthElement();


            if(crossing){
            
            var objects = road.AddObjectsElement();
                var obj = objects.AddObjectElement(s:"35.3000000000000000e+0",t:"99.2300000000000000e-2", zOffset:"-7.3000000000000000e-6");
                    var outline = obj.AddOutlineElement();
            
            }
            

            return road;
        }
    }
}