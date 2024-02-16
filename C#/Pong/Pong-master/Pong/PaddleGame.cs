using System;
using System.Windows.Forms;
using System.Drawing;

public class PaddleGame : Form
{
    private Label vxLabel;
    private Label vzLabel;

    private TextBox vxTextBox;
    private TextBox vzTextBox;

    private Button startButton;
    private Button resetButton;

    private Panel drawingPanel;
    
    //cositas de las fisicas
 
	  private Label vyLabel;
	  private Label distanceLabel;
	  private Label axesLabel;
	  private Label massLabel;
	  private Label areaLabel;
	  private Label cdLabel;
	  private Label densityLabel;
	  private Label windVxLabel;
	  private Label windVyLabel;
	  private Label rxLabel;
	  private Label ryLabel;
	  private Label rzLabel;
	  private Label omegaLabel;
	  private Label radiusLabel;
	  private Label spinAxisLabel;
	
	  private TextBox vyTextBox;
	  private TextBox distanceTextBox;
	  private TextBox massTextBox;
	  private TextBox areaTextBox;
	  private TextBox cdTextBox;
	  private TextBox densityTextBox;
	  private TextBox windVxTextBox;
	  private TextBox windVyTextBox;
	  private TextBox rxTextBox;
	  private TextBox ryTextBox;
	  private TextBox rzTextBox;
	  private TextBox omegaTextBox;
	  private TextBox radiusTextBox;

    //  These fields store ball data and the coefficient 
    //  of restitution.
    private SpinProjectile pilota; // simple ball
    private double ballRadius; //  radius of ball
    private double paddleZ, paddleZ2;
	private double paddleX, paddleX2;
    private int paddleHeight;
    private int paddleWidth;
    
    int dpWidth = 301, dpHeight = 201;

    private Boolean perdut = false; // quan perds!
    

    //  The Timer is used to control the execution speed
    //  of the game.
    private Timer gameTimer;

    public PaddleGame()
    {

        //  Initialize ball location, velocity, and
        //  paddle location.
        pilota = new SpinProjectile(150.0, 150.0, 150.0, 0.0, 0.0, 0.0, 0.0,
                           0.0459, 0.001432, 0.5, 1.225, 0.0, 0.0,
                           0.0, 1.0, 0.0, 300.0, 0.02135);
        ballRadius = 5.0;
        paddleZ = 10;
        paddleZ2 = dpHeight-10;
        paddleX2 = 100;
        paddleX = 0;
        paddleHeight = 10;
        paddleWidth = 65;

        //  Create a Timer object that will be used
        //  to slow the action down.
        gameTimer = new Timer();
        gameTimer.Interval = 50;  //  delay in milliseconds.
        gameTimer.Tick += new EventHandler(ActionPerformed);

        //  Create some Labels
        //fet
        vxLabel = new Label();
        vxLabel.Text = "x-velocity, m/s";
        vxLabel.Font = new Font(vxLabel.Font, FontStyle.Bold);
        vxLabel.Top = 370;
        vxLabel.Left = 10;
        vxLabel.Width = 100;
		
        //fet
        vzLabel = new Label();
        vzLabel.Text = "z-velocity, m/s";
        vzLabel.Font = new Font(vzLabel.Font, FontStyle.Bold);
        vzLabel.Top = 400;
        vzLabel.Left = 10;
        vzLabel.Width = 100;
        
        //fet
	    vyLabel = new Label();
	    vyLabel.Text = "y-velocity, m/s";
	    vyLabel.Font = new Font(vyLabel.Font, FontStyle.Bold);
	    vyLabel.Top = 430;
	    vyLabel.Left = 10;
	    vyLabel.Width = 100;
	
	    //fet
	    massLabel = new Label();
	    massLabel.Text = "mass, kg";
	    massLabel.Font = new Font(massLabel.Font, FontStyle.Bold);
	    massLabel.Top = 430;
	    massLabel.Left = 200;
	    massLabel.Width = 110;
	
	    areaLabel = new Label();
	    areaLabel.Text = "area, m^2";
	    areaLabel.Font = new Font(areaLabel.Font, FontStyle.Bold);
	    areaLabel.Top = 370;
	    areaLabel.Left = 400;
	    areaLabel.Width = 110;
	
	    cdLabel = new Label();
	    cdLabel.Text = "drag coefficient";
	    cdLabel.Font = new Font(cdLabel.Font, FontStyle.Bold);
	    cdLabel.Top = 370;
	    cdLabel.Left = 200;
	    cdLabel.Width = 110;
	
	    densityLabel = new Label();
	    densityLabel.Text = "density, kg/m^3";
	    densityLabel.Font = new Font(densityLabel.Font, FontStyle.Bold);
	    densityLabel.Top = 400;
	    densityLabel.Left = 200;
	    densityLabel.Width = 110;
	
	    windVxLabel = new Label();
	    windVxLabel.Text = "Wind x-velocity, m/s";
	    windVxLabel.Font = new Font(windVxLabel.Font, FontStyle.Bold);
	    windVxLabel.Top = 400;
	    windVxLabel.Left = 400;
	    windVxLabel.Width = 120;
	
	    windVyLabel = new Label();
	    windVyLabel.Text = "Wind y-velocity, m/s";
	    windVyLabel.Font = new Font(windVyLabel.Font, FontStyle.Bold);
	    windVyLabel.Top = 430;
	    windVyLabel.Left = 400;
	    windVyLabel.Width = 120;
	
	    omegaLabel = new Label();
	    omegaLabel.Text = "Angular velocity, rad/s";
	    omegaLabel.Font = new Font(omegaLabel.Font, FontStyle.Bold);
	    omegaLabel.Top = 370;
	    omegaLabel.Left = 630;
	    omegaLabel.Width = 120;
	
	    radiusLabel = new Label();
	    radiusLabel.Text = "Sphere radius, m";
	    radiusLabel.Font = new Font(radiusLabel.Font, FontStyle.Bold);
	    radiusLabel.Top = 400;
	    radiusLabel.Left = 630;
	    radiusLabel.Width = 120;
	
	    spinAxisLabel = new Label();
	    spinAxisLabel.Text = "Spin Axes";
	    spinAxisLabel.Font = new Font(spinAxisLabel.Font, FontStyle.Bold);
	    spinAxisLabel.Top = 340;
	    spinAxisLabel.Left = 870;
	    spinAxisLabel.Width = 120;
	
	    rxLabel = new Label();
	    rxLabel.Text = "rx";
	    rxLabel.Font = new Font(rxLabel.Font, FontStyle.Bold);
	    rxLabel.Top = 370;
	    rxLabel.Left = 870;
	    rxLabel.Width = 50;
	
	    ryLabel = new Label();
	    ryLabel.Text = "ry";
	    ryLabel.Font = new Font(ryLabel.Font, FontStyle.Bold);
	    ryLabel.Top = 400;
	    ryLabel.Left = 870;
	    ryLabel.Width = 50;
	
	    rzLabel = new Label();
	    rzLabel.Text = "rz";
	    rzLabel.Font = new Font(rzLabel.Font, FontStyle.Bold);
	    rzLabel.Top = 430;
	    rzLabel.Left = 870;
	    rzLabel.Width = 50;

        //  Create TextBox objects to display the outcome.
        vxTextBox = new TextBox();
        vxTextBox.Width = 60;
        vxTextBox.Text = String.Format("{0}", 100.0m);
        vxTextBox.AutoSize = true;
        vxTextBox.Top = vxLabel.Top;
        vxTextBox.Left = 110;

        vzTextBox = new TextBox();
        vzTextBox.Width = 60;
        vzTextBox.Text = String.Format("{0}", -80.0m);
        vzTextBox.AutoSize = true;
        vzTextBox.Top = vzLabel.Top;
        vzTextBox.Left = 110;
	
	    vyTextBox = new TextBox();
	    vyTextBox.Width = 60;
	    vyTextBox.Text = String.Format("{0}", 0.0m);
	    vyTextBox.AutoSize = true;
	    vyTextBox.Top =430;
	    vyTextBox.Left = 110;
	
	    massTextBox = new TextBox();
	    massTextBox.Width = 60;
	    massTextBox.Text = String.Format("{0}", 0.0459);
	    massTextBox.AutoSize = true;
	    massTextBox.Top = 430;
	    massTextBox.Left = 320;
	
	    areaTextBox = new TextBox();
	    areaTextBox.Width = 60;
	    areaTextBox.Text = String.Format("{0}", 0.001432);
	    areaTextBox.AutoSize = true;
	    areaTextBox.Top = 370;
	    areaTextBox.Left = 530;
	
	    cdTextBox = new TextBox();
	    cdTextBox.Width = 60;
	    cdTextBox.Text = String.Format("{0}", 0.25);
	    cdTextBox.AutoSize = true;
	    cdTextBox.Top = 370;
	    cdTextBox.Left = 320;
	
	    densityTextBox = new TextBox();
	    densityTextBox.Width = 60;
	    densityTextBox.Text = String.Format("{0}", 1.225);
	    densityTextBox.AutoSize = true;
	    densityTextBox.Top = densityLabel.Top;
	    densityTextBox.Left = 320;
	
	    windVxTextBox = new TextBox();
	    windVxTextBox.Width = 60;
	    windVxTextBox.Text = String.Format("{0}", 10.0);
	    windVxTextBox.AutoSize = true;
	    windVxTextBox.Top = 400;
	    windVxTextBox.Left = 530;
	
	    windVyTextBox = new TextBox();
	    windVyTextBox.Width = 60;
	    windVyTextBox.Text = String.Format("{0}", 0.0m);
	    windVyTextBox.AutoSize = true;
	    windVyTextBox.Top = 430;
	    windVyTextBox.Left = 530;
	
	    omegaTextBox = new TextBox();
	    omegaTextBox.Width = 60;
	    omegaTextBox.Text = String.Format("{0}", 300.0);
	    omegaTextBox.AutoSize = true;
	    omegaTextBox.Top = 370;
	    omegaTextBox.Left = 770;
	
	    radiusTextBox = new TextBox();
	    radiusTextBox.Width = 60;
	    radiusTextBox.Text = String.Format("{0}", 0.02135);
	    radiusTextBox.AutoSize = true;
	    radiusTextBox.Top = 400;
	    radiusTextBox.Left = 770;
	
	    rxTextBox = new TextBox();
	    rxTextBox.Width = 60;
	    rxTextBox.Text = String.Format("{0}", 0.0m); 
	    rxTextBox.AutoSize = true;
	    rxTextBox.Top = 370;
	    rxTextBox.Left = 930;
	
	    ryTextBox = new TextBox();
	    ryTextBox.Width = 50;
	    ryTextBox.Text = String.Format("{0}", 1.0m);
	    ryTextBox.AutoSize = true;
	    ryTextBox.Top = 400;
	    ryTextBox.Left = 930;
	
	    rzTextBox = new TextBox();
	    rzTextBox.Width = 50;
	    rzTextBox.Text = String.Format("{0}", 0.0m);
	    rzTextBox.AutoSize = true;
	    rzTextBox.Top = 430;
	    rzTextBox.Left = 930;

        //  Create Button objects 
        int buttonHeight = 30;
        int buttonWidth = 50;

        startButton = new Button();
        startButton.Text = "Start";
        startButton.Height = buttonHeight;
        startButton.Width = buttonWidth;
        startButton.BackColor = Color.Lime;
        startButton.Top = 30;
        startButton.Left = 475;
        startButton.Click += new EventHandler(StartButtonClicked);

        resetButton = new Button();
        resetButton.Text = "Reset";
        resetButton.Height = buttonHeight;
        resetButton.Width = buttonWidth;
        resetButton.BackColor = Color.Red;
        resetButton.Top = 30;
        resetButton.Left = 550;
        resetButton.Click += new EventHandler(ResetButtonClicked);

        //  Create a drawing panel.
        drawingPanel = new Panel();
        
        drawingPanel.Width = dpWidth;
        drawingPanel.Height = dpHeight;
        drawingPanel.Left = 380;
        drawingPanel.Top = 110;
        drawingPanel.BorderStyle = BorderStyle.FixedSingle;
        //drawingPanel.MouseDown += new MouseEventHandler(MovePaddle);
        
        //drawingPanel.KeyDown += new KeyEventHandler(MovePaddle2);
        //drawingPanel.KeyPress += new KeyEventArgs(MovePaddle2);
        //drawingPanel.KeyDown += new KeyEventArgs(MovePaddle2);
        //drawingPanel.KeyPress += new KeyEventHandler(MovePaddle2);
        //this.KeyDown += new KeyEventHandler(MovePaddle2);
        //this.KeyPress += new KeyEventArgs(MovePaddle2);
        //this.KeyDown += new KeyEventArgs(MovePaddle2);
        //his.KeyPress += new KeyEventHandler(MovePaddle2);
        this.KeyPreview = true;
        this.KeyDown += new KeyEventHandler(MovePaddle2);

        //  Add the GUI components to the Form
        this.Controls.Add(vxLabel);
        this.Controls.Add(vzLabel);
        this.Controls.Add(vyLabel);
        this.Controls.Add(vxTextBox);
        this.Controls.Add(vzTextBox);
        this.Controls.Add(startButton);
        this.Controls.Add(resetButton);
        this.Controls.Add(drawingPanel);
	    this.Controls.Add(massLabel);
	    this.Controls.Add(areaLabel);
	    this.Controls.Add(cdLabel);
	    this.Controls.Add(densityLabel);
	    this.Controls.Add(windVxLabel);
	    this.Controls.Add(windVyLabel);
	    this.Controls.Add(omegaLabel);
	    this.Controls.Add(radiusLabel);
	    this.Controls.Add(spinAxisLabel);
	    this.Controls.Add(rxLabel);
	    this.Controls.Add(ryLabel);
	    this.Controls.Add(rzLabel);
	    this.Controls.Add(vxTextBox);
	    this.Controls.Add(vyTextBox);
	    this.Controls.Add(vzTextBox);
	    this.Controls.Add(distanceTextBox);
	    this.Controls.Add(massTextBox);
	    this.Controls.Add(areaTextBox);
	    this.Controls.Add(cdTextBox);
	    this.Controls.Add(densityTextBox);
	    this.Controls.Add(windVxTextBox);
	    this.Controls.Add(windVyTextBox);
	    this.Controls.Add(omegaTextBox);
	    this.Controls.Add(radiusTextBox);
	    this.Controls.Add(rxTextBox);
	    this.Controls.Add(ryTextBox);
	    this.Controls.Add(rzTextBox);

        // Set the size and title of the form
        this.Width = 1080;
        this.Height = 720;
        this.Text = "PING PONG";
        this.BackColor = Color.Lavender;

        //  Center the form on the screen and make
        //  it visible.
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Visible = true;

        //  Update the GUI display
        UpdateDisplay();
    }

    //  Event handling method for the "Start" button
    public void StartButtonClicked(object source, EventArgs evt)
    {
        //  Get the initial values from the textfield
	    double vx0 = Convert.ToDouble(vxTextBox.Text);
	    double vy0 = Convert.ToDouble(vyTextBox.Text);
	    double vz0 = Convert.ToDouble(vzTextBox.Text);
	    double mass = Convert.ToDouble(massTextBox.Text);
	    double area = Convert.ToDouble(areaTextBox.Text);
	    double cd = Convert.ToDouble(cdTextBox.Text);
	    double density = Convert.ToDouble(densityTextBox.Text);
	    double windVx = Convert.ToDouble(windVxTextBox.Text);
	    double windVy = Convert.ToDouble(windVyTextBox.Text);
	    double rx = Convert.ToDouble(rxTextBox.Text);
	    double ry = Convert.ToDouble(ryTextBox.Text);
	    double rz = Convert.ToDouble(rzTextBox.Text);
	    double omega = Convert.ToDouble(omegaTextBox.Text);
	    double radius = Convert.ToDouble(radiusTextBox.Text);
	    
        pilota = new SpinProjectile(150.0, 150.0, 150.0, vx0, vy0, vz0, 
         0.0, mass, area, density, cd, windVx, windVy,
         rx, ry, rz, omega, radius);

        //  Update the display
        perdut = false;
        UpdateDisplay();

        //  Start the box sliding using a Timer object
        //  to slow down the action.
        gameTimer.Start();
    }

    //  Event handling method for the "Reset" button
    public void ResetButtonClicked(object source, EventArgs evt)
    {
        //  stop the timer.
        gameTimer.Stop();
        perdut = false;
        
        //coger de nuevo los valores
		double vx0 = Convert.ToDouble(vxTextBox.Text);
	    double vy0 = Convert.ToDouble(vyTextBox.Text);
	    double vz0 = Convert.ToDouble(vzTextBox.Text);
	    double mass = Convert.ToDouble(massTextBox.Text);
	    double area = Convert.ToDouble(areaTextBox.Text);
	    double cd = Convert.ToDouble(cdTextBox.Text);
	    double density = Convert.ToDouble(densityTextBox.Text);
	    double windVx = Convert.ToDouble(windVxTextBox.Text);
	    double windVy = Convert.ToDouble(windVyTextBox.Text);
	    double rx = Convert.ToDouble(rxTextBox.Text);
	    double ry = Convert.ToDouble(ryTextBox.Text);
	    double rz = Convert.ToDouble(rzTextBox.Text);
	    double omega = Convert.ToDouble(omegaTextBox.Text);
	    double radius = Convert.ToDouble(radiusTextBox.Text);
	    
        //  Reset the ball location, velocity, and
        //  paddle location.
        pilota = new SpinProjectile(0.0, 0.0, 0.0, vx0, vy0, vz0, 
         0.0, mass, area, density, cd, windVx, windVy,
         rx, ry, rz, omega, radius);
        paddleZ = 10;
        paddleZ2 = dpHeight-10;
        paddleX2 = 100;
        paddleX = 0;

        //  Update the display.
        UpdateDisplay();
    }

    //  This event handling method is called when the
    //  mouse is dragged over the panel. 
    
    
    public void MovePaddle2(object source, KeyEventArgs e)
    {
    	if (e.KeyCode == Keys.A )
    {
    	paddleX-=10;
       
    }
    	else if (e.KeyCode == Keys.D )
    {
       paddleX+=10;
    }
    	
    	
    	if (e.KeyCode == Keys.J )
    {
    	paddleX2-=10;
       
    }
    	else if (e.KeyCode == Keys.L )
    {
       paddleX2+=10;
    }
    	
    }
    
    /*public void MovePaddle(object source, MouseEventArgs evt)
    {

    	
        //  Move the paddle to where the mouse was clicked.
        int Width = drawingPanel.Width - 1;
        paddleX = evt.X;

        //  Update the display.
        UpdateDisplay();
    }*/

    //  This method redraws the GUI display.
    private void UpdateDisplay()
    {
        Graphics g = drawingPanel.CreateGraphics();
        int width = drawingPanel.Width - 1;
        int height = drawingPanel.Height - 1;

        //  Clear the current display.
        g.Clear(Color.White);

        //  Update the position of the spheres on the screen.
        SolidBrush brush = new SolidBrush(Color.Black);

        //  Update the position of the ball on the screen.
        double ballX = pilota.GetX();
        double ballZ = pilota.GetZ();
        int xPosition = (int)(ballX - ballRadius);
        int zPosition = (int)(height - ballRadius - ballZ);
        g.FillEllipse(brush, xPosition, zPosition, 2 * (int)(ballRadius),
                      2 * (int)(ballRadius));

        //  Update the position of the paddle on the screen.
        zPosition = (int)(height - paddleZ);
        //g.FillRectangle(brush, 10, zPosition - paddleHeight / 2,
                       // 10, paddleHeight);
       
                       g.FillRectangle(brush, (float)paddleX, 10,
                       paddleWidth, paddleHeight);
                       
                       g.FillRectangle(brush, (float)paddleX2, (float)paddleZ2-10,
                       paddleWidth, paddleHeight);
                       
        //  missatge quan perds
        if (perdut)
        {
            Font font = new Font("Arial", 12);
            String s = string.Format("You Loose");
            g.DrawString(s, font, brush, 100, 30);
        }
        //  Clean up the Graphics object.
        g.Dispose();
    }
    
    

    //  This method is called by the Timer every 0.05 seconds.
    public void ActionPerformed(object source, EventArgs evt)
    {
    	
    	
    		
        //  Get dimensions of drawing area.
        int width = drawingPanel.Width - 1;
        int height = drawingPanel.Height - 1;

        // Get current ball properties
        double ballX = pilota.GetX();
        double ballZ = pilota.GetZ();
        double ballVx = pilota.GetVx();
        double ballVz = pilota.GetVz();
        double ballVy = pilota.GetVy();
        double ballY = pilota.GetY();
        

	    double mass = Convert.ToDouble(massTextBox.Text);
	    double area = Convert.ToDouble(areaTextBox.Text);
	    double cd = Convert.ToDouble(cdTextBox.Text);
	    double density = Convert.ToDouble(densityTextBox.Text);
	    double windVx = Convert.ToDouble(windVxTextBox.Text);
	    double windVy = Convert.ToDouble(windVyTextBox.Text);
	    double rx = Convert.ToDouble(rxTextBox.Text);
	    double ry = Convert.ToDouble(ryTextBox.Text);
	    double rz = Convert.ToDouble(rzTextBox.Text);
	    double omega = Convert.ToDouble(omegaTextBox.Text);
	    double radius = Convert.ToDouble(radiusTextBox.Text);


        /*if (ballVz<0 && ballZ-ballRadius <= 0.0+ballRadius)
        {
        	
        	pilota.ChangeVZ();
        	Console.WriteLine(ballVz); 
            Console.WriteLine("1"); 
            //perdut = true;
        }

		else if (ballVz >0 && ballZ+ballRadius >= height+ballRadius)
        {
			pilota.ChangeVZ();
			Console.WriteLine(ballVz);
            Console.WriteLine("2");
            //perdut = true;
        }*/
        
		
		if (ballVx>0 && ballX+ballRadius >= width)
        {
			ballVx=-ballVx;
			pilota.ChangeVX();
			//Console.WriteLine(ballVx);
            Console.WriteLine("3");
 
        }
		
		else if (ballVx<0 && ballX-ballRadius <= 0)
        {
			ballVx=-ballVx;
			pilota.ChangeVX();
			//Console.WriteLine(ballVx);
            Console.WriteLine("3");
 
        }
		
		
		else if ((ballZ+ballRadius >= height+ballRadius-20&&(ballX>paddleX&&ballX<paddleX+paddleWidth)) ){
			
			ballVz=-ballVz;
        		pilota.ChangeVZ();
            	Console.WriteLine("5");
		}
		
		else if ((ballZ-ballRadius <= 0+20-ballRadius
		          &&(ballX>paddleX2&&ballX<paddleX2+paddleWidth)
		         ) ){
			ballVz=-ballVz;
        		pilota.ChangeVZ();
            	Console.WriteLine("5");
		}
		
		
		
		
		if (ballZ+ballRadius> height)
        {
			
            Console.WriteLine("simulation stopped");
            gameTimer.Stop();
            perdut = true;
        }
		
		if (ballZ-ballRadius< 0)
        {
			
            Console.WriteLine("simulation stopped");
            gameTimer.Stop();
            perdut = true;
        }
		

        //  Compute the new location of the ball. 
        double timeIncrement = 0.07;
        pilota = new SpinProjectile(ballX, ballY, ballZ, ballVx, ballVy, ballVz, timeIncrement, mass, area, density,   cd, windVx, windVy, rx, ry, rz, omega, radius);
        pilota.UpdateLocationAndVelocity(timeIncrement);


        UpdateDisplay();
    }

    static void Main()
    {
        Application.Run(new PaddleGame());
    }
}
