using System;
using System.Windows.Forms;
using System.Drawing;


public class BeanBag : Form
{
	//etiquetas
	private Label planetLabel;
    private Label vLabel;
    private Label alphaLabel;
    private Label resultadoLabel;
    
    private ComboBox planetComboBox;

    //textbooks
    private TextBox vTextBox;
    private TextBox alphaTextBox;
    private TextBox resultadoTextBook;

    //botones
    private Button fireButton;
    private Button resetButton;

    //drawing panel
    private Panel drawingPanel;

    //imagenes usadas
    private Image playerIcon;
	private Image wallpaper;
    
	//timer
    private Timer gameTimer;

    //fisicas del beangbang
    private bagBall bBall;
    
     
    int windowdHeight = 720;
    int windowWith = 1280;
    
    double initialX;
    double initialZ;

    double alpha;
    
    int separation=50;
    float horizontLine=330;
    
    double gravedad;

    string selectedItem;
    
    int rectX, rectWidth, rectHeight, rectY;
    double v;
    
    Random rand = new Random();
    
    
    public BeanBag()
    { 	
		rectX=200;
    	rectWidth=50;
    	rectHeight=rectWidth;
    	rectY=(int)horizontLine-rectHeight;
    	
    	
    	wallpaper = Image.FromFile("tierra.png");
    	initialZ=1.7;
    	initialX=0.5;
    	
        //  Initialize the beanbag parameters.
        bBall = new bagBall(initialX, initialZ, 0.0, 0.0);

        //  Set up images
        
        playerIcon = Image.FromFile("hombre1.png");

        //  Create a Timer object that will be used
        //  to slow the action down.
        gameTimer = new Timer();
        gameTimer.Interval = 50;  //  delay in milliseconds.
        gameTimer.Tick += new EventHandler(ActionPerformed);

        //  Create some Labels
        vLabel = new Label();
        vLabel.Text = "Velocidad (modulo)";
        vLabel.Font = new Font(vLabel.Font, FontStyle.Bold);
        vLabel.Top =  separation;
        vLabel.Left = 10;
        vLabel.Width = 180;

        
        alphaLabel = new Label();
        alphaLabel.Text = "Alpha (grados)";
        alphaLabel.Font = new Font(vLabel.Font, FontStyle.Bold);
        alphaLabel.Top = separation+vLabel.Top;
        alphaLabel.Left = 10;
        alphaLabel.Width = 180;


        
        planetLabel = new Label();
        planetLabel.Text = "Planeta";
        planetLabel.Font = new Font(planetLabel.Font, FontStyle.Bold);
        planetLabel.Top = 230;
        planetLabel.Left = 10;
        planetLabel.Width = 50;
        
        resultadoLabel = new Label();
        resultadoLabel.Text = "Resultado: ";
        resultadoLabel.Font = new Font(resultadoLabel.Font, FontStyle.Bold);
        resultadoLabel.Top = 200;
        resultadoLabel.Left = 10;
        resultadoLabel.Width = 70;

        //Create TextBox objects to display the outcome.
        vTextBox = new TextBox();
        vTextBox.Width = 50;
        vTextBox.Text = String.Format("{0}", 2.5m);
        vTextBox.AutoSize = true;
        vTextBox.Top = vLabel.Top;
        vTextBox.Left = 200;


        
        alphaTextBox = new TextBox();
        alphaTextBox.Width = 50;
        alphaTextBox.Text = String.Format("{0}", 2.0m);
        alphaTextBox.Top = alphaLabel.Top;
        alphaTextBox.Left = 200;


        
        resultadoTextBook = new TextBox();
        resultadoTextBook.Width = 150;
        resultadoTextBook.Text = " ";
        resultadoTextBook.Top = resultadoLabel.Top;
        resultadoTextBook.Left = 80;
        
        
        //  Create Button objects 
        int buttonHeight = 50;
        int buttonWidth = 100;
        int buttonLeft = 120;

        fireButton = new Button();
        fireButton.Text = "Fire";
        fireButton.Height = buttonHeight;
        fireButton.Width = buttonWidth;
        fireButton.Top = 300;
        fireButton.Left = buttonLeft;
        fireButton.BackColor = Color.Yellow;
        fireButton.Click += new EventHandler(FireButtonClicked);

        resetButton = new Button();
        resetButton.Text = "Reset";
        resetButton.Height = buttonHeight;
        resetButton.Width = buttonWidth;
        resetButton.Top = 400;
        resetButton.Left = buttonLeft;
        resetButton.BackColor = Color.Orange;
        resetButton.Click += new EventHandler(ResetButtonClicked);

        //  Create a drawing panel.
        drawingPanel = new Panel();
        drawingPanel.Width = windowWith/2;
        drawingPanel.Height = windowdHeight/2;
        drawingPanel.Left = drawingPanel.Width/2;
        drawingPanel.Top = drawingPanel.Height/2;
        drawingPanel.BorderStyle = BorderStyle.FixedSingle;

        planetComboBox = new ComboBox();
        planetComboBox.Items.Add("Tierra");
        planetComboBox.Items.Add("Luna");
        planetComboBox.Items.Add("Sol");
        planetComboBox.Items.Add("Jupiter");
        planetComboBox.SelectedIndex = 0;
        planetComboBox.Left = 80;
        planetComboBox.Top = 230;
        
        //  Add the GUI components to the Form
        this.Controls.Add(vLabel);
        this.Controls.Add(vTextBox);
        this.Controls.Add(fireButton);
        this.Controls.Add(resetButton);
        this.Controls.Add(drawingPanel);
        this.Controls.Add(alphaLabel);
        this.Controls.Add(alphaTextBox); 
        this.Controls.Add(planetComboBox);
        this.Controls.Add(planetLabel);
        this.Controls.Add(resultadoLabel);
		this.Controls.Add(resultadoTextBook);
        
        // Set the size and title of the form
        this.Height = windowdHeight;
        this.Width = windowWith;
        this.Text = "Basket Game";
        this.BackColor = Color.Lavender;

        //  Center the form on the screen and make
        //  it visible.
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Visible = true;

        //  Update the GUI display
        UpdateDisplay();
    }

    
    //  Event handling method for the "Fire" button
    public void FireButtonClicked(object source, EventArgs e)
    {
    	
    	
    	
    	selectedItem = (string)planetComboBox.SelectedItem;
        if (String.Equals(selectedItem, "Tierra"))
        {
        	wallpaper = Image.FromFile("tierra.png");
            gravedad = 9.81;
        }
        else if (String.Equals(selectedItem, "Luna"))
        {
        	wallpaper = Image.FromFile("luna.png");
            gravedad = 1.624;
        }
        else if(String.Equals(selectedItem, "Sol"))
        {
        	wallpaper = Image.FromFile("sol.png");
            gravedad = 274;
        }
        else
        {
        	wallpaper = Image.FromFile("jupiter.png");
            gravedad = 24.79;
                              }
    	
    	
    	
    	playerIcon = Image.FromFile("hombre2.png");
   
    	
    	alpha = Convert.ToDouble(alphaTextBox.Text);
        v = Convert.ToDouble(vTextBox.Text);
        //double vz0 = Convert.ToDouble(vzTextBox.Text);
        bBall = bBall = new bagBall(0.5, 0.5, v, 0.0);
        gameTimer.Start();
        //draw=false;	
    
    }
    
    public void ResetButtonClicked(object source, EventArgs e)
    {
    	rectX= rand.Next(100, drawingPanel.Width-rectWidth);
    	resultadoTextBook.Text=" ";
    	
    	playerIcon = Image.FromFile("hombre1.png");
        gameTimer.Stop();
        bBall = new bagBall(0.5, 0.5, 0.0, 0.0);
        //draw=true;
        UpdateDisplay();
    }
	public int zPosition;
   	public int xPosition;
	Graphics g;
    public void UpdateDisplay()
    {
    	g = drawingPanel.CreateGraphics();
		Pen blackPen = new Pen(Color.Black, 2);
		
		
		int width = drawingPanel.Width - 1;
        int height = drawingPanel.Height - 1;
		SolidBrush brush = new SolidBrush(Color.Black);
 
        
        int zLocation = 125 - 67;
        double x = bBall.GetX();
        double z = bBall.GetZ();
        SolidBrush orangeBrush = new SolidBrush(Color.Orange);
         xPosition = (int)(100.0 * x);
        double deltaZ = z - 1.25;
        zPosition = (int)(125 - 100.0 * deltaZ);
        
        
        
        g.DrawImage(wallpaper, 0, zLocation, drawingPanel.Width, drawingPanel.Height+40);
        //if(draw){
        
        g.DrawImage(playerIcon, 7, horizontLine-140, 150, 160);
        //}
        
        Rectangle rect = new Rectangle(rectX, rectY, rectWidth, rectHeight);
         //Rectangle rect = new Rectangle((int)horizontLine, (int)horizontLine-50, 50, 50);
        g.DrawRectangle(blackPen, rect);
    

	
        
        g.FillEllipse(orangeBrush,xPosition, zPosition, 15, 15);

       
        //g.Dispose();
       
    }
    
    
    //  This method is called by the Timer every 0.05 seconds.
    public void ActionPerformed(object source, EventArgs e)
    {
        double timeIncrement = 0.05;
        bBall.UpdateLocationAndVelocity(timeIncrement, alpha, v, gravedad);
        
        UpdateDisplay();
        
       System.Diagnostics.Debug.WriteLine(xPosition);
        
        
        if ((zPosition<= rectY+rectHeight)&&(zPosition>= rectY)&&((xPosition >=  rectX)&&(xPosition <=  rectX+rectWidth))){
         	resultadoTextBook.Text="Has perdido";
         	gameTimer.Stop();
        }
        
        else if(zPosition>horizontLine){
        	resultadoTextBook.Text="Has ganado";
        
         	gameTimer.Stop();
        }
    }
    
    static void Main()
    {
    	Application.Run(new BeanBag());
    }
}
