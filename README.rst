=========================================
Autonomous Driving Simulation Environment
=========================================

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
=================

.. contents::


Description
===========

The goal of this international team project was the development of a Traffic Network Builder that allows for the on-the-fly creation of maps in Opendrive 1.4 format. These maps can then be used in simulators like Carla or other projects based on it.

1. `The OpenDRIVE Standard <https://www.asam.net/standards/detail/opendrive/>`__ - A standardized format for representing road networks, used to create maps for autonomous driving simulations.
2. `Carla Driving Simulator <https://carla.org/>`__ - An open-source autonomous driving simulator that enables researchers and developers to experiment with various algorithms in a realistic environment.
3. `AI Testing Simulator <https://github.com/jodi106/AI_Testing_Simulator/>`__ - The previous international collaboration which this project is based on


How to set up
=============

How to set up the Autonomous-Driving-Simulator-Environment (ADSE) on Windows 
---------------------------------------------------------------------------

1. Download the ADSE.zip file from the latest release notes on GitHub: `Releases <https://github.com/tropper26/Autonomous-Driving-Simulation-Environment/releases>`_  
2. Extract the downloaded ADSE.zip file to your desired location  
3. To start the application, open the Unity Map Editor.exe file. Accept any pop-ups that appear during installation

How to set up Carla 0.9.13 on Windows
-------------------------------------

We recommend using the Package Installation 0.9.13 for a convenient and quick setup. Future versions might cause conflicts (not tested).

1. Create a New Python Environment

2. Read the "Before you begin" section in the `Carla documentation <https://carla.readthedocs.io/en/0.9.13/start_quickstart/>`_ to check prerequisites.

3. Install the required Python modules "pygame" and "numpy" as well as the "carla" package to your Python environment using the following commands::

.. code-block:: python

   pip3 install --user pygame numpy
   pip3 install carla

4. Download and Unzip Carla 0.9.13 Package

   - Download the package version 0.9.13 from `Github <https://github.com/carla-simulator/carla/releases/tag/0.9.13>`_.
   - Extract the downloaded package to a desired location.

For more detailed instructions and information on Linux setup, refer to the official `Carla Documentation <https://carla.readthedocs.io/en/0.9.13/start_quickstart/>`_.



How to use
==========

How to use the Unity Map Editor on Windows
-------------------------------------------------------------------------

Create a road network by dragging road pieces onto the map

For a list of useful keyboard shortcuts, click the "Help" button in the bottom right corner. To close the help, click the "Help" button again

Once you've finished creating the road network, export the map to an .XODR format: Click the "Export" button in the top right corner. You will receive a confirmation of your export

All exported maps will be saved in the "ADSE/Unity Map Editor_Data/Maps" directory

Developer documentation
-----------------------

* `Doxygen Website <https://tropper26.github.io/Autonomous-Driving-Simulation-Environment/>`_

Credits
=======

This project started in February 2023 as a cooperation between University of Mannheim and UBB Cluj.
The team consists of:

* `David <https://github.com/tropper26/>`_
* `Eva <https://github.com/eva128/>`_
* `Yannick <https://github.com/YannickLa/>`_
* `Dragos <https://github.com/Gustyx/>`_
* `Mircea <https://github.com/IlinaMn/>`_
* `Andrada <https://github.com/andradademian/>`_

