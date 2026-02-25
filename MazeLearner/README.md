# Maze Learner

### Progression: --%

## Descriptions
<p>Explore dungeons of mazes, learn throughtout in mazes, encounter a monster of mazes and fight for it to survive in the maze, A endless maze that seek the greatest in the end, Fight the monster inside of the maze using the knowledge of the hero, answer carefully or you step in the doom.</p>
<hr>

### Early Development
- During development stage of the game we dicussed about the how sprites will be looked like
- I provide some sprites I made during early development of Solarized project and some unused sprites on my working mod

<hr>

### Credits:
- RyiSnow (Tutorial)
- Monogame 2D Starter Kit (Framework)
- Mojang (Logic/Methods for screens and containers)
- Re-Logic (Logic/Methods)
- ICutter (Example using TiledCS Lib)
- NaakkaDev (Example using TiledCS Lib)
- TheBoneJarmer (TiledCS Lib)
- Lightbulb15 (Tileset)
- PSDK (Assets, Audio and Logic/Methods)
- TheVamp (More simple logic in Terraria Source Code)
- WelseyFG (HGSS Tilesets)
- Flurmimon (Nature Tilesets)

<hr>

### Members
- Ruales, Vincent John <b>(Programmer / Art and Design)</b>
- Rowque, Romwel <b>(Art and Design)</b>
- Valencia, Ronel <b>(Programmer / Documentaions)</b>

<hr>

### Development Progress
- Implemented a sprites and basic movements
- Added Animated sprites
- Added Overlay UIs
- Added Interaction Box / Collision Box
- Added Collision Detection for preventing player move on not passable tiles and other entity
- Added Dialogs
- Added Basic movement Behaviors Walking, Idle and Looking
- Added some configurations on dialog (TODO: Implement this after creating a settings and main menu)
- Fix the configuration that not being load the value of json files
- Added Icons Yipee!!
- Splash Screen and Title Screen Added!
- Adding Logger or logs files
- Added Battle System not fuctioning yet
	- Math Battle is now implemented
		- Only the Calculator Answer
- Added Animation State and Sprites
	- Animation State
		- Can define total frames of the will be playing
		- Allow to configure the speed of the animation (Not Implemented) (Scrapped!)
	- Sprites
		- Where all the character or sprites is draw with the help of animation state defining the frames
- Added Bag Screen
	- Added Menus:
		- Added Tutorial Keybinds for navigating 
		- Inventory
		- Save
		- Emote
		- Settings
		- Exit to Menu (Title Screen)
- Added Maps for the games.
- Added Audio to the game
	- Added Audio Controls 
- Added Multiple Backgrounds
	- Each start of the game, there's 5 background will be played and it stay until the game closed 
- Updated the splash screen
- Added Option
- Improving Bag UIs
- Implement Science Subject (Removed) 
- Adding more monsters
- Added Player Data and Player Creation (Not being implement but instead W.I.P)
- Improving Battle System Screen
- Added Map Culling 
	- Now the map will render only on the screen
- Improve FPS and rendering
- Improving Dialog system
- Improved the Main Menu Screen
- Added Collective
	- Added 8 Collective Items (not obtainable yet)
- Added Label and category for Options 
- Added Player Creation
	- Add 5 save slots
	- Added to able choose Gender 
	- Added to able set own username
	- Added confirmation
- Added Text by Text in dialog
- Improve Battle System UI
- Added Choices in Battle System
	- Fight
		- Normally choices 4  answer in given questions! 
	- Item
		- Allow to use item like healing or etc but the player will deal damage by 1 
	- Run Away
		- Deal 1 Damage to player
			- (TODO) Either remove the npc or npc will stay until the it beat! 
- Added 3x Speed Mechanics
- Improve the rendering again mostly in setting assets to load
- Adding more controls in Entry Buttons
	- Its now a class instead of record class 
- Added Event Maps Flags
	- Event Flags are objects placed they are the one responsible like teleporting spawninng npc and items
	- still in basic and buggy
	- Will added animation in warp flags
		- Transition like fade out screen
- Added FileData and PlayerFileData
	- To create a encrypted database for player so cant change the stats of the player to cheat
	- Status: WIP 
- Improve UI in Player Creation Screen.
	- Added Unique Button for Male and Female
- Fix some issue in Entry menus
	- Added Anchor (Left, Right, Center) 
		- Anchoring the text
	- Fix where if texture have no additional height than original bounding box will not do a hover effect
 - Added Input Box
	- Support by A-Z keywords with 0-9 digits and abc for small words and ABC for capital words
	- Support Ok to confirm to submit to the system what written the input box and Back to remove by 1 of words in input box
	- Added Key Shift to trigger the capital and Press again to disable it
	- Added Key Enter to confirm the text or submit it.
- Added Shaders
	- Known Shaders (Game Screen Shaders) 
	- Game Screen Shaders (WIP)
		- Handle for Day and System and Dark Cave System (Not Implemented) 
- Added Player Creation
	- Have only 5 slots to save
	- Player will pick which gender and name
	- Player will start with 300 coins and 10 Health
- Added some in fail safe in code
	- Added variable Max Health instead
		- Determine the limit of what npc health can have
- Added Day and Night System
- ----(2/07/2026)-----
- Added Knight
- Revised and change some code in the game
- Instead of using Assets Loader to get all npc and player sprites now being called in the main
- Added Array that list down all npcs are being registered
	- Allow to call by using Ids only
- Implementing IDs
	- Some have Ids instead of class and name, we using IDs to call it
- Remove some classes and method not being used
- Removed Math and Science Entity
- Renamed SubjectEntity -> HostileEntity
	- Some code from SubjectEntity being moved to NPC class
- Improve AI behaviors and Added AITypes
	- Knowm AI Types:
		- 0 : NOAI Do nothing, cant be interacted
		- 1 : Look Around
		- 2 : Walk Around
		- 3 : Circle Around
-  Added AI Styles (Not Implemented)
	- AI Stlyes are flags use in NPCs
	- Know AI Styles
		- 0 : Normal
		- 1 : Running
		- 2 : Nan
- Renamed NpcCategory to QuestionType
	- All QuestionType
		- Grammar
		- Vocabulary
		- Structure
		- Comprehension
- More Improve on Tiles
	- Known Issues
		- Some tiles are still being rendered on below of players even their depth are high (Fixed)
- Option now functioning
	- Both main menu and ingame 
- Added Warping between maps
- Added Entity Spawn using Map Flags
- Added Map Transition
	- Sound Effect
	- Fade away (Not Implemented)
	- Known Issue: Npcs still visible even on different map (working on it!)
- Added double secured fail safe for OpenAL Audio when is null
- (2/11/2026)
- Remove Save Slot
- PlayerCreation on Play State
	- Remove the 5 slots replaced with Login And Create 
- Added Login And Register
	- Registering will save their data like previous method
	- Inputting wrong username will invalid and try again!
	- 
- Added Graphic Folder
	- This contain renderering, sprites and animations
- Added SpriteViewMatrix
	- Status: WIP
	- this is the upgraded version of the game camera view hopefully will work as i intended too
- Fix some in NPC and Monsters
- Added Function "RenderDialogs(SpriteBatch sprite, string message)"
	- To fully render a dialog with typing text
- Added Death Timer to all Monsters
- Added More Properties In Event Flags
	- NPC Flags
		- Added AI Types
		- Dialog
		- Battle (Bool Value: 0/1)
- Improve Loggers Messaging and Add Info/Debug/Warn/Error
- Added Sprite Shaders
- Renamed TilesetRenderer to Tiled
	- (Plan: Renaming Again to World or Level)
- Expermenting 3D Arrays from npc, items and objects
	- Note: I discovered this when i created a player for second times and loaded the player will be on same maps and data of previous one
	- Example: NPCS[PlayerIndex -> "What index or slot of player"][MapsId -> Current Map being][Added NPC -> What Slot they been added]

- (02/16/2023)
- Added a Interior Tilesets
- Added a School Tilesets
- Added Intro Map
- Cleanup code
- Added RegisterContent
	- Where all npcs, objects, player, questions, collectives, and maps is being register
- Added Female and Male Sprites
	- Female (Still using Male Sprites)
	- Male (Done)
- Added Map Transition
	- Screen will fade away within 3 seconds??
- Small changes on facing direciton box
	- Made smaller to fit a gap within a hitbox of tiles
- Added some unique collision tiles
- (2/17/2026)
- Text Manager where all string is being call to render is being reworked and redesign
	- Remove some unused methods
	- Renamed from TextManager to Texts
	- Text renamed to DrawString
	- Removing scale
- Reomve all unused fonts asset
- Renamed DialogTextL to Text (Default Font now)
- Renamed DialogTextXL to InputBoxText
- Added 100 Questions
	- Bro whatttt theeee helllllllllllllllllllllllllllll 
- Added Health and Name in Tiled Editor
	- For customizing 
	- Replace the object with a images for more defining and visual not just a box
- Rework all the game systems
	- NPC Spawning
	- Monster Spawning
	- Items Spawning
	- How Npc/Monster/Items added on game
- Added warp object into entity instead of internal warp from tiled
- Seperate the object entity from npc class, object are now indepentends and have own unique attributes
- (2/21/2026)
- Redesign the portfolio
- Fix and made adjustment in battle ui
	- the questions are now on top of the game
	- the questionnair are now below and its now row and column format instead of list down items
	- the questions font are now dialog font format
	- the npc portfolio are now depends what value they put in the Tiled Editor "NpcSpawnObject.BattleImage"
- Added sound for in-battle
- Added Score point
- Added player name and score points above of health
- The Score points are now depends what value they put in the Tiled Editor "NpcSpawnObject.ScorePoints"
	- If player loses they will deduct of by half of original score point drop of every battler npc
- Fix of where all saved data have null
- Made adjustment on entry menus
	- If no text the arrow will now centered on the rectangle box instead of aligning on text
- Fixing the particles
	- Not showing/playing yet when they are called
- (2/22/2026)
- Added Enum Slider
	- Use left or right to select different value based on the enum has been put in. 
- Added Window Mode Options
	- Window: Default 
	- Borderless: Remove border of the screen
	- Fullscreen: Max resolution of game base on the width and height of the monitor
- Fix where save options are not being saving changes files
- Added Player Male and Female Portofoliozzzzxczx
- Remove the player name and score points onxz cthe bagscreen
- Inventory are not stil functioning but have UIs within
- Finally fix the Pause when Background
- Fully function fullscreen
- Fix how the fullscreen config not being sync in the config.json
- 0.65
- Added Notes in Bag
- Added Player Portfolio on Bag
- Added Player Stats such as name, coins, score, and health
- Still fixing on centered camera when minimized version
- Added that you can go in inventory
- Added Faded Black Screen on Bag
- Fix where npc and objects spawn always at 0.5 coordinates ex: tiles is 19 -> npc/objects will spawn at 19.5
- Particles are now visible and playing
	- Issues: Offset on position giving (Fixed)
	- Fixed: Particles are now align on below / center tiles when spawned
	- Adjust the particles rendering, now it will render behind of object/npc's sprites
	- Particles Rendering Method same as the npc layering order
- Fix where player being teleport on spawn object
- 0.66
- Added Text Command
	- Putting name="": will display the given out of the string in dialog
	- Putting Npc.Name will display the interacted npc display name
	- Putting Player.Name will display the player name
	- Putting Emote.Play(id, character_id (need match on tiled), y_offset) will play particles (TBA)
	- Putting Wait.(time) will pause the game within given value (WIP)
	- Putting Do.Fight will put the interacted npc and player in battle (WIP)
	- Putting Do.Fight(character_id) will put the given npc id (need match on tiled) and player in battle (WIP)
- Text Dialog
	- made adjustment adding static field to globally put in the UI Renderer
	- Note: this purpose to not use forloop to search all the npcs list to get the interacted npc
	- made adjustment how the text being render!
	- Added input name, Input name serve what or who they talking to
	- Remove next dialog index instead replace by static field in Main.cs "TextDialogIndex"
	- Note: this purpose to serve into globally
- 0.67 & 0.68
- Added Shake screen in Camera
	- ShakeTick per tick of the shake screen
	- ShakeDuration how much the shake will last
	- ShakeScreen a flag tell to start the shake screen effect
- Changes in Battle
	- Move the question inside of the box instead of having on top
	- Redesign how player name and health and enemy show their hp
	- The questionnaire is now on half of the width of box
	- if the questionnaire is long and overlapping on box will display only ... (example A. The fox jump over...) and will display on the top of the box fully content of selected questionnaire
	- Added damage tint when dealing damage to npc if player is taken damage the camera will shake
- Adjusmtment
	- Flag to npc where if they lose in battle either remove on game or stayed 
- (02/25/2026) v0.68.1
- Adjustment 
	- Remove Camera Shake instead of replace by Black Tint Screen Fading Away
- Redesign the Battle UI and Question
- Added Tooltips for Battle UI
- Added Category Level of each questions
- Added Flags for TiledEditor NPCSpawnObject
	- BattleLevel from 0-2 (0: Lvl 1, 1: Lvl 2, 2: Lvl 3)
	- QuestionCategory from 0-9
- Remove Flags for TiledEditor NPCSpawnObject
	- X and Y properties (Im using layer.X and layer.Y with calculation of tile position)
- Remove Main.TextDialogIndex instead readded the npc.DialogIndex
<hr>

### Monsters
- Gloos
- Knight
- Apex Ape
- Inky
- Spirit
- Vengeful Wisp
- Possessed Book
- Gentlementor

### NPCs
- Guide
- Shop Keeper
- Blacksmith
- Explorer 
- Red (Reference)
- Devs

<hr>

### Software Used
- Microsoft Visual Code
- Paint.Net
- Paint Sai 2
- Tiled
- Notepad
- Visual Code Studio

<hr>

### Keybinds
- Forward: Arrow Up
- Downward: Arrow Down
- Left: Arrow Left
- Right: Arrow Right
- Interact: Z / Enter
- Interact 2/Cancel: X
- Bag: C
- Cancel Bag: X
- Speed 3x: Space (Completed)
<hr>

### TODO:
- Adding EntitySaveData
	- Each player or index have own unique save data of their maps
	- this will prevent the entity of being respawn again and the old data will be applicable and their when they play again
	- In this only differences is the if the  npc Battle are set on 1 they will spawn each load of map
	- if the npc Battle set on 0 they will be stayed and not despawn also the key game objects too will be not despawn


- Removing 5 save slots
- Implenting Local Database
	- Allow to create player database
		- Storing all stats and scores
	- Logging with username and password(?) will gain access to player database
	- Slot slot is only limit of 1000 

- Added a more variety battle system
	- Complete Spelling Bee
	- Questioning
	- Puzzle
	- Scramble Words


- Implementing Day and Night system (Done)
- Implementing Item Drops of Monster (Scrapped)
- Adding Player Stats (Scrapped)

- Adding more UI in Bag Menu
	- Player Name
	- Coins
	- Weapon Holding (Scapped)

- Adding internal code words (Not Implemented)
	- Internal Code words are the function for emoting
		- Ex 1: :happy: -> will play happy particle
- Adding player save data formatted as .dat or .json
- Implementing using arrow keys to other containers/screen (Done)
- Adding more maps
	- Biomes
		- Alpine (Snowy Tundra Cave) (WIP)
		- Forest (WIP)
		- Plains (WIP)
		- Cave  (Added)
		- Castle (WIP)
- Adding more monsters
	- Each have unique effects and look based on the subject and the biomes
- Adding Science Subject (Removed)
- Math Subject  (Removed)
	- Numbers and Algebra
	- Measurment and Geometry
	- Data and Probability

<hr>

		