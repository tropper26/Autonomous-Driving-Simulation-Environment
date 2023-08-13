import argparse
import carla

vertex_distance = 0.1  # in meters
max_road_length = 10.0 # in meters
wall_height = 1.0      # in meters
extra_width = 0.6      # in meters

# Parse command line arguments
parser = argparse.ArgumentParser()
parser.add_argument("xodr_file", help="Path to the XODR file")
args = parser.parse_args()

with open("opendrive/" + args.xodr_file, "r") as f:
    xodr_xml = f.read()

client = carla.Client('localhost', 2000)

world = client.generate_opendrive_world(
    xodr_xml, carla.OpendriveGenerationParameters(
        vertex_distance=vertex_distance,
        max_road_length=max_road_length,
        wall_height=wall_height,
        additional_width=extra_width,
        smooth_junctions=True,
        enable_mesh_visibility=True))
