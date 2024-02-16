using System;

public class SpinProjectile : WindProjectile
{
  private double rx;     //  spin axis vector component
  private double ry;     //  spin axis vector component
  private double rz;     //  spin axis vector component
  private double omega;  //  angular velocity, m/s
  private double radius; //  sphere radius, m

  public SpinProjectile(double x0, double y0, double z0, 
             double vx0, double vy0, double vz0, double time,
             double mass, double area, double density, double Cd,
             double windVx, double windVy, double rx, double ry, 
             double rz, double omega, double radius) :
               base (x0, y0, z0, vx0, vy0, vz0, time, mass, 
                     area, density, Cd, windVx, windVy) {

    //  Initialize variables declared in the SpinProjectile class.
    this.rx = rx;
    this.ry = ry;
    this.rz = rz;
    this.omega = omega;
    this.radius = radius;
  }

  //  These properties are used to access the fields declared
  //  in the class.
  public double Rx {
    get {
      return rx;
    }
  }

  public double Ry {
    get {
      return ry;
    }
  }

  public double Rz {
    get {
      return rz;
    }
  }

  public double Omega {
    get {
      return omega;
    }
  }

  public double Radius {
    get {
      return radius;
    }
  }

  //  The GetRightHandSide() method returns the right-hand
  //  sides of the six first-order projectile ODEs
  //  q[0] = vx = dxdt
  //  q[1] = x
  //  q[2] = vy = dydt
  //  q[3] = y
  //  q[4] = vz = dzdt
  //  q[5] = z
  public override double[] GetRightHandSide(double s, double[] q, 
                              double[] deltaQ, double ds,
                              double qScale) {
    double[] dQ = new double[6];
    double[] newQ = new double[6];

    //  Compute the intermediate values of the 
    //  dependent variables.
    for(int i=0; i<6; ++i) {
      newQ[i] = q[i] + qScale*deltaQ[i];
    }

    //  Declare some convenience variables representing
    //  the intermediate values of velocity.
    
    double vx = newQ[0];
    double x = newQ[1];
    double vy = newQ[2];
    double y = newQ[3];
    double vz = newQ[4];
    double z = newQ[5];

    //  Aquí hi va la física. Heu d'indicar l'increment dQ de 
    // cada variable
    
    double ax = 0;
    double ay = 0;
    double az = G;
    
    //variables para calcular la velocidad aparente
    double v_ax = vx - this.WindVx;
    double v_ay = vy - this.WindVy;
	double v_az = vz;
	
	//velocidad aparente
	double v_a = Math.Sqrt(v_ax*v_ax + v_ay*v_ay + v_az*v_az) + 1.0e-8;
	
	//velocidad normal
	double velocity=Math.Sqrt(vx*vx + vy*vy + vz*vz) + 1.0e-8;
	
	//variables para calcular el drag
	double densidad = this.Density;
    double area = this.Area;
    double c_d = this.Cd;
    double masa = this.Mass;
    
    //calculamos la fuerza drag
	double F_d = 0.5*densidad*area*c_d*v_a*v_a;
	double F_dx = -F_d*v_ax/v_a;
	double F_dy = -F_d*v_ay/v_a;
	double F_dz = -F_d*v_az/v_a;
	
	//Fuerza magna
	double c_l = radius*omega/velocity;
	double F_m = 0.5*densidad*area*c_l*velocity*velocity;
	double F_mx = (vy*rz - ry*vz)*F_m/velocity;
	double F_my = -(vx*rz - rx*vz)*F_m/velocity;
	double F_mz = (vx*ry - rx*vy)*F_m/velocity;
	
	//  Compute the right-hand sides of the six ODEs
	dQ[0] = ds*(F_dx + F_mx)/masa;
	dQ[1] = ds*vx;
	dQ[2] = ds*(F_dy + F_my)/masa;
	dQ[3] = ds*vy;
	dQ[4] = ds*(G + (F_dz + F_mz)/masa);
	dQ[5] = ds*vz;
	
	return dQ;
  }
}