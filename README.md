MonogameTest
============

First project with Monogame and C#

http://rbwhitaker.wikidot.com/monogame-tutorials

Personal notes:

New monogame project -> template doesn't matter

-- GAME LOOP --

Initialize -> Load Content ->

 -> Update -> Draw 
       ^         |
       |         |
       |-------<-v

XNA Content Pipeline: New Monogame Content Project 
("Content Content" will contain the real contents)

-Add reference to main game project
 (under references section)
http://rbwhitaker.wdfiles.com/local--files/monogame-managing-content/AddReference.png

Content Type	File Types
3D Models	.x, .fbx
Textures/Images	.bmp, .dds, .dib, .hdr, .jpg, .pfm, .png, .ppm, .tga
Audio	        .xap (an XACT audio project), .wma, .mp3, .wav
Fonts		.spritefont
Effects		.fx

-drag/drop assets into "ContentContent"
-can make folders


In LoadContent():
Texture2D image = Content.Load<Texture2D>("[FileNameWithoutExtension]");
Texture2D image = Content.Load<Texture2D>("Backgrounds/Logo");


to unload:
Content.Unload();


-- 2D GRAPHICS AND SPRITEBATCH --

2D GFX:
(0, 0) -> x
   | 
   v
   y


A SpriteBatch contains methods for drawing 
groups of sprites onto the screen. (SpriteBatch class draws Texture2D images)
*.Begin();
*.Draw(Texture2D, new Rectangle(x, y, width, height), Color.White);
*.Draw(Texture2D, new Vector2(x, y), Color.White);
*.End();
SpriteBatch.Draw() method:
http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.graphics.spritebatch.draw.aspx

The order that you draw sprites is important. 
Whatever you draw first will be drawn below the later things.

-- SPRITEFONT --
A sprite font takes a normal, vector-based font,
and turns it into a bitmapped font that can be drawn quickly.
.ttf -> .xml
Fonts you can use and sell (redistribute freely, commercially):
kooten
Lindsey*
Miramo
Miramob
Peric
Pericl
Pesca
Pescab
More fonts here: http://www.dafont.com/

To add:
Content explorer -> Add New Item -> VIsual C# -> Sprite Font (xml file)
replace line#14: <FontName>_____</FontName>, with desired font; 
must be in C:/Windows/Fonts
line#20: size; edit other options similarly.


font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

Use spriteBatch to *.DrawString(font, "TEXT", new Vector2(x, y), Color._);
We can concatenate the "text" by doing "text" + variable.



-- TEXTURE ATLAS --
Conglomeration of different poses of a sprite (jumping, walking, etc.)

Make AnimatedSprite class:
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

-- INPUT --
Mouse:
this.IsMouseVisible = true; // put into constructor
private MouseState oldState; // private member variable

MouseState newState = Mouse.GetState(); // -> Update()
if(newState.LeftButton == ButtonState.Pressed
	&& oldState.LeftButton == ButtonState.Released) {}
// swap Pressed and Released; current is activate on button down;
	swap -> activate on button up
oldState = newState;

MouseState class has properties:
int x = mouseState.X;
int y = mouseState.Y;

To SET mouse location (annoy players!):
Mouse.SetPosition(x, y);

Keyboard:
KeyboardState state = Keyboard.GetState();
bool leftArrowKeyDown = state.IsKeyDown(Keys.Left); // or...
if(state.IsKeyDown(Keys.Left)) {}
// but this will fire 60 bullets/second...
// instead do this:
private KeyBoardState oldState; // as a member variable
// in Update():
KeyboardStte newState = Keyboard.GetState();
if(oldState.IsKeyUp(Keys.Left) && newState.IsKeyDown(Keys.Left)) {}
oldState = newState; 
// this way, it will only "fire one bullet per key press"

To access all pressed keys:
Keys[] pressedKeys = state.GetPressedKeys();


-- MOVING THINGS IN A CIRCLE --
update the position like so:
position = new Vector2((float)Math.cos(angle) * distance, (float)Math.Sin(angle) * distance);

-- ADDITIVE BLENDING --
spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

------------------
SUMMARY
------------------
SpriteBatch draws the sprites and the font
Update and Draw will loop around each other
Be creative when taking user input

Important methods:
Content.Load<type>("..."); type = Texture2D, SpriteFont; "..." = file name
*.DrawString(variable, "text", new Vector2(x,y), Color._);
*.Draw(variable, new Vector2(x,y), Color._);
