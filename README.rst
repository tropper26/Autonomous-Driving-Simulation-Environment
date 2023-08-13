Autonomous Driving Simulation Environment
==========================================

.. list-table::
   :widths: 100
   :header-rows: 1

   * - Exported Unity Map
   * - .. image:: https://github.com/tropper26/Autonomous-Driving-Simulation-Environment/blob/main/img/unity_map.png
         :width: 100%
         :alt: Unity Map
         :align: center

.. list-table::
   :widths: 50 50
   :header-rows: 1

   * - Carla Map
     - OpenDRIVE Map
   * - .. image:: https://github.com/tropper26/Autonomous-Driving-Simulation-Environment/blob/main/img/carla_map.png
         :width: 100%
         :alt: Carla Map
         :align: center
     - .. image:: https://github.com/tropper26/Autonomous-Driving-Simulation-Environment/blob/main/img/openDRIVE-viewer_map.png
         :width: 100%
         :alt: OpenDRIVE Map
         :align: center





Table of Contents
#################

.. contents::


Description
###########

The goal of this international team project was the development of a Traffic Network Builder that allows for the on-the-fly creation of maps in Opendrive 1.4 format. These maps can then be used in simulators like Carla or other projects based on it.

1. `The OpenDRIVE Standard <https://www.asam.net/standards/detail/opendrive/>`__ - A standardized format for representing road networks, used to create maps for autonomous driving simulations.
2. `Carla Driving Simulator <https://carla.org/>`__ - An open-source autonomous driving simulator that enables researchers and developers to experiment with various algorithms in a realistic environment.
3. `AI Testing Simulator <https://github.com/jodi106/AI_Testing_Simulator/>`__ - The previous internation collaboration which this project is based on


How to set up
#############

How to Set Up Carla 0.9.13 on Windows
=====

We recommend using the Package Installation 0.9.13 for a convenient and quick setup. Future versions might cause conflicts (not tested).

1. Create a New Python Environment

2. Read the "Before you begin" section in the `Carla documentation <https://carla.readthedocs.io/en/0.9.13/start_quickstart/>`_ to check prerequisites.

3. Install the required Python modules "pygame" and "numpy" as well as the "carla" package to your Python environment using the following commands::

   pip3 install --user pygame numpy
   pip3 install carla

4. Download and Unzip Carla 0.9.13 Package

   - Download the package version 0.9.13 from `Github <https://github.com/carla-simulator/carla/releases/tag/0.9.13>`_.
   - Extract the downloaded package to a desired location.

For more detailed instructions and information on Linux setup, refer to the official `Carla Documentation <https://carla.readthedocs.io/en/0.9.13/start_quickstart/>`_.



How to use
##########

`Code documentation <https://tropper26.github.io/Autonomous-Driving-Simulation-Environment/>`__

Credits
#######

This project started in February 2023 as a cooperation between University of Mannheim and UBB Cluj.
The team consists of:

* `David <https://github.com/tropper26/>`__
* `Eva <https://github.com/eva128/>`__
* `Yannick <https://github.com/YannickLa/>`__
* `Dragos <https://github.com/Gustyx/>`__
* `Mircea <https://github.com/IlinaMn/>`__
* `Andrada <https://github.com/andradademian/>`__

