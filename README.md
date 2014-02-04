PipesGame
=========

update at 2014-02-04.

## Game Flow

1. open scene at Assets/Scenes/Game_Core.unity
1. Start Game
1. NGUI : loading
1. NGUI : choose singleplay or multiplay mode
1. NGUI : acount and password
1. `GAME_VIEW` acitvated
1. `GAME_VIEW/Level 1` acitvated 
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in right *Container* , `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump.Element.workActivate` is acitvated ( firework particle
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in other *Container* , it wiil be no effect. 
1. When `GAME_VIEW/_CircuitMaster.Circuit.isAllWork` is true, `GAME_VIEW/Level 1.CircuitSet.successActivate` is acitivated ( firework particle in the millde of screen
1. deactivate `GAME_VIEW/Level 1` , and activae `GAME_VIEW/Level 2`
1. so on the level 2 - 4 .
1. When `GAME_VIEW/_CircuitMaster.Circuit.isAllWork` is true in *Level 4* , it will go nowhere any more .
1. the end

## Time record 

`GAME_VIEW/_CircuitMaster.timeRecord` asign to a NGUI label , to show what time it has gone .

## Animation GameObjects to exchange

* `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump.Element.workActivate`
* `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump.Element.brokenActivate`
* `GAME_VIEW/Level 1/Circuit Succeed`
* `GAME_VIEW/Level 2/DYNAMIC_ITEMS/B_AirExtractor.Element.workActivate`
* `GAME_VIEW/Level 2/DYNAMIC_ITEMS/B_AirExtractor.Element.brokenActivate`
* `GAME_VIEW/Level 2/DYNAMIC_ITEMS/C_DirtExtractor.Element.workActivate`
* `GAME_VIEW/Level 2/DYNAMIC_ITEMS/C_DirtExtractor.Element.brokenActivate`
* `GAME_VIEW/Level 2/Circuit Succeed`
* `GAME_VIEW/Level 3/DYNAMIC_ITEMS/D_FillingRegulator.Element.workActivate`
* `GAME_VIEW/Level 3/DYNAMIC_ITEMS/D_FillingRegulator.Element.brokenActivate`
* `GAME_VIEW/Level 3/DYNAMIC_ITEMS/E_Filter.Element.workActivate`
* `GAME_VIEW/Level 3/DYNAMIC_ITEMS/E_Filter.Element.brokenActivate`
* `GAME_VIEW/Level 3/Circuit Succeed`
* `GAME_VIEW/Level 4/DYNAMIC_ITEMS/F_TemperatureStabilizer.Element.workActivate`
* `GAME_VIEW/Level 4/DYNAMIC_ITEMS/F_TemperatureStabilizer.Element.brokenActivate`
* `GAME_VIEW/Level 4/DYNAMIC_ITEMS/G_PressureStabilizer.Element.workActivate`
* `GAME_VIEW/Level 4/DYNAMIC_ITEMS/G_PressureStabilizer.Element.brokenActivate`
* `GAME_VIEW/Level 4/Circuit Succeed`
* `GAME_VIEW/_CircuitMaster.timeRecord` for time record.