# ImmigrationSim

A simulation of an immigration checkpoint built in Unity 6, modelling traveller flow through Security and Immigration stages.

## Engine Version

Unity 6000.3.16f1

## Setup Instructions

1. Download repo as zip. Extract all files. I had issues with cloning so I'd recommend just downloading as zip.
2. Open the project in Unity 6000.3.16f1 via Unity Hub
3. No additional dependencies are required

## Run Instructions

1. In the Unity Editor, open the **Setup Scene** (`Assets/Scenes/SetupScene`)
2. Configure the simulation parameters via the UI panel
3. Press **Play**, scene will transition automatically into the Main Scene and begin running
4. Output is shown in a live dashboard on the right
5. When the simulation ends a banner will appear at the top of the screen

## Build Instructions

1. Go to **File → Build Settings**
2. Ensure both **SetupScene** and **MainScene** are added to the Scenes In Build list, with SetupScene at index 0
3. Select your target platform and click **Build**

## Modifying Simulation Parameters

### Via the Setup Scene UI

Launch the Setup Scene and adjust parameters directly in the configuration panel before starting the simulation. Changes take effect on the next run.

### Via SimConfig ScriptableObject

1. In the Project window, locate the `SimConfig` ScriptableObject
2. Select it and modify values directly in the Inspector
3. These values will be used as defaults when the Setup and Main Scene loads

#### Parameter Reference

| Parameter                                             | Description                                                                          |
| ----------------------------------------------------- | ------------------------------------------------------------------------------------ |
| Travellers Per Second                                 | Arrival rate (γ) — baseline is 0.87 traveller/sec                                    |
| Sim Speed                                             | Time multiplier — e.g. 10x runs the simulation 10× faster                            |
| Sim Duration                                          | Observation window in seconds                                                        |
| Citizen Ratio                                         | Percentage of spawned travellers that are Citizens (vs Foreigners)                   |
| Wait Threshold (s)                                    | Threshold used to calculate % of travellers exceeding acceptable wait time           |
| Security Server Count                                 | Number of active Security counters                                                   |
| Citizen/Foreigner Min/Max Security Processing Time    | Uniform distribution bounds for Security stage processing time per traveller type    |
| Immigration Server Count                              | Number of active Immigration counters                                                |
| Citizen/Foreigner Min/Max Immigration Processing Time | Uniform distribution bounds for Immigration stage processing time per traveller type |
| Secondary Screening Probability (%)                   | Chance a traveller at Immigration undergoes additional screening                     |
| Min/Max Secondary Screening Time                      | Additional time added if secondary screening is triggered                            |
