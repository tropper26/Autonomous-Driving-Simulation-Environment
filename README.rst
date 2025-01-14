=========================================
Traffic Network Builder
=========================================

.. image:: /media/traffic_network_builder.gif
   :width: 100%
   :alt: Traffic Network Builder Demo
   :align: center

.. list-table::
   :widths: 33 33 33
   :header-rows: 1

   * - Exported Map
     - OpenDRIVE Map
     - Carla Map
   * - .. image:: /media/unity_map.png
         :width: 100%
         :alt: Unity Map
         :align: center
     - .. image:: /media/openDRIVE-viewer_map.png
         :width: 100%
         :alt: OpenDRIVE Map
         :align: center
     - .. image:: /media/carla_map.png
         :width: 100%
         :alt: Carla Map
         :align: center






Table of Contents
=================

.. contents::


Description
===========

The goal of this international team project was the development of a Traffic Network Builder that allows for the on-the-fly creation of maps in Opendrive 1.4 format. These maps can then be used in simulators like Carla or other projects based on it.

1. `The OpenDRIVE Standard <https://www.asam.net/standards/detail/opendrive/>`__ - A standardized format for representing road networks, used to create maps for autonomous driving simulations.
2. `Carla Driving Simulator <https://carla.org/>`__ - An open-source autonomous driving simulator that enables researchers and developers to experiment with various algorithms in a realistic environment.
3. `AI Testing Simulator <https://github.com/jodi106/AI_Testing_Simulator/>`__ - The previous international collaboration which this project is extending.


Setup
=============

How to set up the Traffic Network Builder on Windows 
---------------------------------------------------------------------------

1. Download the zip file from the latest release notes on GitHub: `Releases <https://github.com/tropper26/Autonomous-Driving-Simulation-Environment/releases>`_  
2. Extract the downloaded zip file to your desired location  
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

5. Copy the contents of the 'carla_scripts' folder into <CARLA_0.9.x/WindowsNoEditor/PythonAPI/examples>

For more detailed instructions and information on Linux setup, refer to the official `Carla Documentation <https://carla.readthedocs.io/en/0.9.13/start_quickstart/>`_.



How to use
==========

How to use the Unity Map Editor on Windows
-------------------------------------------------------------------------

1. Create a road network by dragging road pieces onto the map (Screenshot - 1.1)

2. For a list of useful keyboard shortcuts (Screenshot - 1.3), click the "Help" button in the bottom right corner (Screenshot - 1.2). To close the help, click the "Help" button again.

3. Once you've finished creating the road network, export the map to an .XODR format: Click the "Export" button in the top right corner (Screenshot - 1.4). You will receive a confirmation of your export  (Screenshot - 1.5)

4. All exported maps will be saved in the <Maps> directory

.. list-table::
   :widths: 100
   :header-rows: 1

   * - Unity Map Editor
   * - .. image:: /media/unity_map_editor.png
         :width: 100%
         :alt: Unity Map Editor
         :align: center

How to run exported Map in CARLA
--------------------------------

1. Add your exported XODR file to <CARLA_0.9.x/WindowsNoEditor/PythonAPI/examples/opendrive>

2. Run the following command in the examples folder::

.. code-block:: python

   python .\generate_opendrive_map.py <YourFileNameHere>.xodr

Many more examples can be found in the `opendrive folder <./carla_scripts/opendrive>`_

Developer documentation
-----------------------

* `Doxygen Website <https://tropper26.github.io/Autonomous-Driving-Simulation-Environment/>`_


Known Issues
============

* As of Carla 0.9.13, the generated maps are not fully compatible with the simulator. This implementation relies on Carla's integration of ASAM OpenDRIVE. 
Even though not clearly stated in the docs (`https://carla.readthedocs.io/en/latest/adv_opendrive/`), the simulator seems to implement an early version of the standard leaving many features unsupported:

* Road markings are not visible
* Lanes marked as parking do not generate
* The exact position of traffic signs & lights does not seem to translate correctly from the openDRIVE format
* Spawnpoints are do not always generate correctly

Solving these issues would require making changes to the Carla simulator itself which requires a different set of skills and resources that are not available at this time. As of now, the project is on hold.


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
