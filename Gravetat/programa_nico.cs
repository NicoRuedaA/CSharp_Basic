using System;
//ventanas
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
//teclado
using System.Windows.Input;

public class GravityGame : Form
{
	//etiqueta planeta velocidad y resultados
    private Label planetLabel;
    private Label velocityInitialLabel;
    private Label resultsLabel;
    private Label accelerationLabel;
    private Label boxActualVelocityLabel;
    private Label boxMaxVelocityLabel;

    //texto del resultado
    private TextBox resultsTextBox;
    //texto de la velocidad
    private TextBox initialVelocityTextBox;
    private TextBox accelerationTextBox;
    private TextBox actualVelocityTextBox;
    private TextBox maxBoxVelocityTextBox;

    //botones
    private Button startButton;
    private Button dropButton;
    private Button resetButton;
    private Button pauseButton;
    private Button leftButton;
    private Button rightButton;

    //lista de planetas
    private ComboBox planetComboBox;

	//cuadrado donde se imprimen los objetos    
    private Panel drawingPanel;

    //gameTimer controla el tiempo de ejecución
    private Timer gameTimer;

    //anchura del cuadro
    private int boxWidth;
   	private int boxWidthAux;
    private int textBoxWeidth, textBoxLeft;
    
    //parametro y de la bola
    private double ballAltitude;
    //parametro X de la bola
    private double ballLocation;
    //altitud inicial de la bola
    private double initialAltitude; 
    //gravedad
    private double g;
    //tiempo desde que se comenzó a mover la caja
    private double dropTime;
    //true si la bola ha sidosoltada
    private double ballVelocity; 
    
    private int puntuacion;
	private int contador;
	private double time;
    private double timeIncrement;
	private double sentido;
    
    private bool dropped;
    private bool pausado;
    //idnica si la caja va hacia la izquierda o la derecha
    private bool gameStarted;
	//indica si la pelota va hacia arriba
	private bool subiendo;

	
	private int buttonWidth = 100;
	private int buttonHeight = 30;
	private int buttonTop = 100;
        
	private int windowHeight =720;
	private int windowWidth = 1280;
    
    private double ballLocationInicial;
    private double boxLocationInitial;
    

    
    //posicion de las copias de la caja para que aparezca por el lado contrario al salir de 
    //la pantalla
    private double boxLocationLeft1, boxLocationLeft2, boxLocationRight;
    private double boxAcceleration;
    
	private double boxVelocity;
    private double boxVelocityAux;
    private double maxBoxVelocity;
    private double maxBoxVelocityAux;
    private double boxInitialVelocity;
    private double boxLocation;
double ballSentido;
double ballAcceleration;

    
	int subiendoValor;
    
    //para generar valores aleatorios
    Random rd = new Random();
 
    public GravityGame()
    {

    	textBoxWeidth = 115;
    	textBoxLeft = 150;
    	
        //Parametros iniciales de la caja y la pelota
        boxLocation = 0.0;
        boxWidth = 100;
        boxWidthAux = boxWidth;
        initialAltitude = 120.0;
        ballAltitude = initialAltitude;
        ballLocation = 210;
        time = 0.0;
        dropTime = 0.0;
        dropped = false;
        
        //creamos un "cronometro" que simulará el tiempo
        gameTimer = new Timer();
        gameTimer.Interval = 50;  //  delay in milliseconds.
        gameTimer.Tick += new EventHandler(ActionPerformed);

        
        //  CREAMOS LAS LABELS
        planetLabel = new Label();
        planetLabel.Text = "Planeta";
        planetLabel.Font = new Font(planetLabel.Font, FontStyle.Bold);
        planetLabel.Top = 30;
        planetLabel.Left = 10;
        planetLabel.Width = 50;

        velocityInitialLabel = new Label();
        velocityInitialLabel.Text = "Initial Box velocity (m/s)";
        velocityInitialLabel.Font = new Font(velocityInitialLabel.Font, FontStyle.Bold);
        velocityInitialLabel.Top = 500;
        velocityInitialLabel.Left = 10;

        resultsLabel = new Label();
        resultsLabel.Text = "Resultado";
        resultsLabel.Font = new Font(resultsLabel.Font, FontStyle.Bold);
        resultsLabel.Top = 550;
        resultsLabel.Left = 10;
        resultsLabel.Width = 75;
        
        accelerationLabel = new Label();
        accelerationLabel.Text = "Aceleracion (m/s^2)";
        accelerationLabel.Font = new Font(accelerationLabel.Font, FontStyle.Bold);
        accelerationLabel.Top = 600;
        accelerationLabel.Left = 10;
        accelerationLabel.Width = 75;

        
        boxActualVelocityLabel = new Label();
        boxActualVelocityLabel.Text = "Actual Box Velocity (m/s)";
        boxActualVelocityLabel.Font = new Font(boxActualVelocityLabel.Font, FontStyle.Bold);
        boxActualVelocityLabel.Top = 650;
        boxActualVelocityLabel.Left = 10;
        boxActualVelocityLabel.Width = 75;
        
        
        boxMaxVelocityLabel = new Label();
        boxMaxVelocityLabel.Text = "Max box velocity (m/s)";
        boxMaxVelocityLabel.Font = new Font(boxMaxVelocityLabel.Font, FontStyle.Bold);
        boxMaxVelocityLabel.Top = 450;
        boxMaxVelocityLabel.Left = 10;
        
        
        //la velocidad de la caja
        initialVelocityTextBox = new TextBox();
        initialVelocityTextBox.Width = textBoxWeidth;
        initialVelocityTextBox.Text = String.Format("{0}", 5.0m);
        initialVelocityTextBox.AutoSize = true;
        initialVelocityTextBox.Top = velocityInitialLabel.Top;
        initialVelocityTextBox.Left = textBoxLeft;

        //resultado
        resultsTextBox = new TextBox();
        resultsTextBox.Width = textBoxWeidth;
        resultsTextBox.Text = "";
        resultsTextBox.AutoSize = true;
        resultsTextBox.Top = resultsLabel.Top;
        resultsTextBox.Left = textBoxLeft;

        
       	//aceleracion
       	accelerationTextBox = new TextBox();
        accelerationTextBox.Width = textBoxWeidth;
        accelerationTextBox.Text = String.Format("{0}", 5.0m);
        accelerationTextBox.AutoSize = true;
        accelerationTextBox.Top = accelerationLabel.Top;
        accelerationTextBox.Left = textBoxLeft;

		
        actualVelocityTextBox = new TextBox();
        actualVelocityTextBox.Width = textBoxWeidth;
        actualVelocityTextBox.Text = "";
        actualVelocityTextBox.AutoSize = true;
        actualVelocityTextBox.Top = boxActualVelocityLabel.Top;
        actualVelocityTextBox.Left = textBoxLeft;
        
        maxBoxVelocityTextBox = new TextBox();
        maxBoxVelocityTextBox.Width = textBoxWeidth;
        maxBoxVelocityTextBox.Text = String.Format("{0}", 15.0m);
        maxBoxVelocityTextBox.AutoSize = true;
        maxBoxVelocityTextBox.Top = boxMaxVelocityLabel.Top;
        maxBoxVelocityTextBox.Left = textBoxLeft;

      	//boton start
        startButton = new Button();
        startButton.Text = "Comenzar";
        startButton.Height = buttonHeight;
        startButton.Width = buttonWidth;
        startButton.Top = buttonTop;
        startButton.Left = 390;
        startButton.Click += new EventHandler(StartButtonClicked);

        //boton drop
        dropButton = new Button();
        dropButton.Text = "Soltar";
        dropButton.Height = buttonHeight;
        dropButton.Width = buttonWidth;
        dropButton.Top = buttonTop;
        dropButton.Left = startButton.Left + dropButton.Width + 10;
        dropButton.Click += new EventHandler(DropButtonClicked);

        //boton reset
        resetButton = new Button();
        resetButton.Text = "Reiniciar";
        resetButton.Height = buttonHeight;
        resetButton.Width = buttonWidth;
        resetButton.Top = buttonTop;
        resetButton.Left = dropButton.Left + resetButton.Width + 10;
        resetButton.Click += new EventHandler(ResetButtonClicked);
        
        //boton pause
        pauseButton = new Button();
        pauseButton.Text = "Pause";
        pauseButton.Height = buttonHeight;
        pauseButton.Width = buttonWidth;
        pauseButton.Top = buttonTop;
        pauseButton.Left = resetButton.Left + pauseButton.Width + 10;
        pauseButton.Click += new EventHandler(PauseButtonClicked);
        
        //left button
        leftButton = new Button();
        leftButton.Text = "<";
        leftButton.Height = buttonHeight;
        leftButton.Width = buttonWidth;
        leftButton.Top = velocityInitialLabel.Top;
        leftButton.Left = (resetButton.Left + pauseButton.Width + 10)/2;
        leftButton.Click += new EventHandler(ButtonLeftClicked);
        
        //right button
        rightButton = new Button();
        rightButton.Text = ">";
        rightButton.Height = buttonHeight;
        rightButton.Width = buttonWidth;
        rightButton.Top = velocityInitialLabel.Top;
        rightButton.Left = (resetButton.Left + pauseButton.Width + 10);
        rightButton.Click += new EventHandler(ButtonRightClicked);

        // Combobox
        planetComboBox = new ComboBox();
        planetComboBox.Items.Add("Tierra");
        planetComboBox.Items.Add("Luna");
        planetComboBox.Items.Add("Jupiter");
        planetComboBox.Items.Add("Namek");
        planetComboBox.Items.Add("Tatooine");
        planetComboBox.Items.Add("Darwin IV");
        planetComboBox.SelectedIndex = 0;
        planetComboBox.Left = 80;
        planetComboBox.Top = planetLabel.Top;

        //espacio donde se imprime la caja y el circulo
        drawingPanel = new Panel();
        drawingPanel.Width = 501;
        drawingPanel.Height = 251;
        drawingPanel.Left = windowWidth/2 - drawingPanel.Width/2;
        drawingPanel.Top = windowHeight/2 - drawingPanel.Height/2 - 50;
        drawingPanel.BorderStyle = BorderStyle.FixedSingle;

        //añadimos elementos a la GUI
        this.Controls.Add(planetLabel);
        this.Controls.Add(velocityInitialLabel);
        this.Controls.Add(resultsLabel);
        this.Controls.Add(initialVelocityTextBox);
        this.Controls.Add(resultsTextBox);
        this.Controls.Add(startButton);
        this.Controls.Add(dropButton);
        this.Controls.Add(resetButton);
        this.Controls.Add(pauseButton);
        this.Controls.Add(planetComboBox);
        this.Controls.Add(drawingPanel);
        this.Controls.Add(accelerationLabel);
        this.Controls.Add(accelerationTextBox);
        this.Controls.Add(boxActualVelocityLabel);
        this.Controls.Add(actualVelocityTextBox);
        this.Controls.Add(leftButton);
        this.Controls.Add(rightButton);
        this.Controls.Add(boxMaxVelocityLabel);
        this.Controls.Add(maxBoxVelocityTextBox);

        //Tamaño del título
        this.Height = windowHeight;
        this.Width = windowWidth;
        this.Text = "Juego de gravedad";

        //centramos la ventana y lo hacemos visible
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Visible = true;

        //Actualizar la GUI
        UpdateDisplay();
    }

	
    
    public void ButtonLeftClicked(object source, EventArgs e){
    	if(sentido>0){
    	//velocidadpositiva=false;	
    	sentido=-1;
    	
    	}
    }
    
    public void ButtonRightClicked(object source, EventArgs e){
    	if(sentido<0){
    	//velocidadpositiva=true;
    	sentido=1;
    	
    	}
    	
    }
    
	
    public void StartButtonClicked(object source, EventArgs e)
    {
    	ballAcceleration = g/20;
    	ballSentido=0;
    	contador =20;
    	sentido =1;
    	ballLocationInicial = ballLocation;
    	
    	boxLocationInitial = drawingPanel.Width;
    	boxLocation=boxLocationInitial;
    	//maxBoxVelocityLocation=0;
    	//locationAux=0;

		subiendoValor=-1;
    	subiendo=false;
    	gameStarted = true;
    	dropped = false;
    	gameStarted = true;

		pausado = false;
		
		
    	time = 0.0;
        dropTime = 0.0;
    	puntuacion = 0;
        
		ballLocation = rd.Next(0,500);
    	ballAltitude = initialAltitude;

		
        //variables de fisicas
        
        
        
		
        //ballVelocity = (maxBoxVelocity/4)*-1;
        
        
        boxInitialVelocity = Convert.ToDouble(initialVelocityTextBox.Text);
        boxVelocityAux=boxInitialVelocity; 
        maxBoxVelocity = Convert.ToDouble(maxBoxVelocityTextBox.Text);
        maxBoxVelocityAux = maxBoxVelocity;
        boxAcceleration = Convert.ToDouble(accelerationTextBox.Text);
        ballVelocity=10;
        
        
        //determinar que planeta hemos cogido y heredar su gravedad
        string selectedItem = (string)planetComboBox.SelectedItem;
        if (String.Equals(selectedItem, "Tierra"))
        {
            g = 9.81;
        }
        else if (String.Equals(selectedItem, "Luna"))
        {
            g = 1.624;
        }
        else if(String.Equals(selectedItem, "Jupiter"))
        {
            g = 24.8;  //  Jupiter
        }
        else if(String.Equals(selectedItem, "Namek"))
        {
            g = 83.2;
        }
        else if(String.Equals(selectedItem, "Tatooine"))
        {
            g = 7.6; 
        }
        else
        {
            g = 0.6;  //  Darwin IV
        }

		resultsTextBox.Text = "";

		
		boxWidthAux=boxWidth;
        //comenzamos el timer
        gameTimer.Start();
    }

    //botón soltar la pelota
    public void DropButtonClicked(object source, EventArgs e)
    {
    	//bola soltada 
        dropped = true;
    }

    //botón para resetear
    public void ResetButtonClicked(object source, EventArgs e)
    {
    	puntuacion = 0;
    	time = 0.0;
        dropTime = 0.0;
        boxVelocity=0;
    	subiendo=false;
    	gameStarted = false;
    	dropped = false;

		ballLocation = rd.Next(0,500);
    	pausado = true;
    	ballAltitude = initialAltitude;
    	resultsTextBox.Text = "";
    	actualVelocityTextBox.Text = Convert.ToString("0");
        //paramos el timer
       
        gameTimer.Stop();
		boxLocation = 0.0;
        
        boxWidth=boxWidthAux;

        //actualizamos la GUI
        UpdateDisplay();
    }
    
    
    
    //funcion del boton pause
    public void PauseButtonClicked(object source, EventArgs e)
    {  
    	if(gameStarted){
	    	pausado = !pausado;
	    	if(pausado){
	    		gameTimer.Stop();
	    	}
	    	else gameTimer.Start();
    	}
    }
    
    

    //valores de lo imprimido en la GUI
    private void UpdateDisplay()
    {
    	
    	
        Graphics g = drawingPanel.CreateGraphics();
        int width = drawingPanel.Width - 1;
        int height = drawingPanel.Height - 1;
        string selectedItem = (string)planetComboBox.SelectedItem;
        if (String.Equals(selectedItem, "Tierra"))
        {
            drawingPanel.BackColor = Color.Red;
        }
        else if (String.Equals(selectedItem, "Luna"))
        {
            drawingPanel.BackColor = Color.Blue;
        }
        else if(String.Equals(selectedItem, "Jupiter"))
        {
            drawingPanel.BackColor = Color.Yellow;
        }
        else if(String.Equals(selectedItem, "Namek"))
        {
            drawingPanel.BackColor = Color.Orange;
        }
        else if(String.Equals(selectedItem, "Tatooine"))
        {
            drawingPanel.BackColor = Color.Gray;
        }
        else
        {
            drawingPanel.BackColor = Color.Black;
        }
        

        //  Clear the current display.
        
        g.Clear(drawingPanel.BackColor);

        Pen blackPen = new Pen(Color.AliceBlue, 2);
        g.DrawLine(blackPen, 0, 135, width, 135);

        //  Update the position of the box and
        //  ball on the screen.
        SolidBrush brush = new SolidBrush(Color.White);
        g.FillRectangle(brush, (int)boxLocation, 120, boxWidth, 15);
        g.FillRectangle(brush, (int)boxLocationLeft1, 120, boxWidth, 15);
        g.FillRectangle(brush, (int)boxLocationLeft2, 120, boxWidth, 15);
        g.FillRectangle(brush, (int)boxLocationRight, 120, boxWidth, 15);

        int zPosition = (int)(initialAltitude - ballAltitude);
        g.FillEllipse(brush, (int)ballLocation, zPosition, 15, 15);

        //  Clean up the Graphics object.
        g.Dispose();
    }

    //esta funcion se llama cada 0.05 segundos
    public void ActionPerformed(object source, EventArgs e)
    {
    	Debug.Write(g);
    	
    	
    	if(ballLocation<0||ballLocation>=drawingPanel.Width){
    		ballSentido=-ballSentido;
    	}
    	
    	
    	boxLocationLeft2=boxLocation-drawingPanel.Width;
    	boxLocationLeft1=boxLocation-(drawingPanel.Width)*2;
    	boxLocationRight=boxLocation+drawingPanel.Width;
    	
    	
    	//el temporizador comienza a contar al principio del juego, no necesetia mas condiciones
		//cada vez que se ejcuta la funcion se suma 1 al contad
		timeIncrement = 0.05;
        time += timeIncrement;
        
        
        //el tiempo que lleva cayendo la bola solo se inicializa cuando le damos al boton de soltar
        if(dropped) {
        	dropTime += timeIncrement;
        	if(ballVelocity<=10){
        		ballVelocity+=ballAcceleration*time;
        	}
        	ballLocation += ballSentido*ballVelocity;
        	ballAltitude += ballVelocity*(subiendoValor);
        }
       
    	
        //la caja sale por un lado y aparece por el otro
    	if(boxLocationLeft2>=((drawingPanel.Width)*2)-boxWidth){
    		
    		boxLocation=boxLocationInitial-boxWidth;
    	}
    	if(boxLocation<=(-boxWidth)){
    		
    		boxLocation=boxLocationInitial-boxWidth;
    	}
        
         
        if(boxVelocity>=maxBoxVelocity/20){
        	boxVelocity=maxBoxVelocity;
        	boxLocation += boxVelocity*(sentido);
        	
        }
        else{
	    	if(contador<20){
	        	boxLocation += boxVelocityAux*(sentido);
	        	contador++;
	        }
	        //Debug.Write(contador + " ");
	        
	        if(contador==20){
	        	
	        	//entre 20 porque esta funcion se ejecuta 20 veces en un segundo
	        	contador=0;
	        }
	        
	        if(contador==0){ 
	        	boxVelocity=boxInitialVelocity+boxAcceleration*time;
				boxVelocityAux=boxVelocity;
				boxVelocity=boxVelocity/20;
	        }
        }
        
        


     	
        
        
        //mostramos por pantalla la velocidad actual
        actualVelocityTextBox.Text = Convert.ToString(boxVelocity);
        
        
        //**********MOVIMIENTO DE LA PELOTA**********
        
        
        
        
        
        

        //la pelota toca la plataforma
        if (!subiendo&&
            ((ballAltitude <= 15.0&&ballAltitude>=5) &&
	            	(
	             	(ballLocation > boxLocation && ballLocation < boxLocation+boxWidth)||
					(ballLocation > boxLocationLeft2 && ballLocation < boxLocationLeft2+boxWidth)||
					(ballLocation > boxLocationLeft1 && ballLocation < boxLocationLeft1+boxWidth)||
					(ballLocation > boxLocationRight && ballLocation < boxLocationRight+boxWidth)				
	           		)
	            )
        	)
        	{
        	ballSentido=-sentido;
        	boxWidth-=boxWidth/8;
        	subiendo=true;
        	subiendoValor=1;
        	//dropTime = 2;
        	puntuacion++;
        	
			resultsTextBox.Text = "Puntuacion: " + puntuacion;
        }
        
        
        //si la bola toca el suelo
        else if (ballAltitude <= 0.0)
        {
        	//paramos el juego
    		EndGame();
    
        }
        
        
        //la boca toca el techo
        if(subiendo&&(ballAltitude>120)) {
        	subiendo=false;
        	subiendoValor=-1;
        }
        
        UpdateDisplay();
    }
 
    void EndGame(){
    	gameStarted = false;
            gameTimer.Stop();
            resultsTextBox.Text = "Ups! Puntos: " + puntuacion;
    }

    static void Main()
    {
    	//iniciar la aplicación
        Application.Run(new GravityGame());
    }
}
