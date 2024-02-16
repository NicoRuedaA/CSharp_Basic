using System;

public class SimpleProjectile : ODE
{
  public const double G = -9.81;

  public SimpleProjectile(double x0, double y0, double z0, 
                          double vx0, double vy0, double vz0,
                          double time) : base(6) {

    this.S = time;
    SetQ(vx0,0);
    SetQ(x0, 1);
    SetQ(vy0,2);
    SetQ(y0, 3);
    SetQ(vz0,4);
    SetQ(z0, 5);
  }

  public double GetVx() {
    return GetQ(0);
  }

  public double GetVy() {
    return GetQ(2);
  }

  public double GetVz() {
    return GetQ(4);
  }

  public double GetX() {
    return GetQ(1);
  }

  public double GetY() {
    return GetQ(3);
  }

  public double GetZ() {
    return GetQ(5);
  }

  public double GetTime() {
    return this.S;
  }
  

  

	public virtual void UpdateLocationAndVelocity(double dt) {
		ODESolver.RungeKutta4(this, dt);
		
	
		
		
		
	}


	public override double[] GetRightHandSide(double s, double[] q, 
		double[] deltaQ, double ds,
		double qScale) 
	{
		double[] dQ = new double[6];
		double[] newQ = new double[6];


		for(int i=0; i<6; ++i) {
			newQ[i] = q[i] + qScale*deltaQ[i];
		}

		double vx = newQ[0];
		double vy = newQ[2];
		double vz = newQ[4];
 
		dQ[0] = 0;
		dQ[1] = ds*vx;
		dQ[2] = 0;
		dQ[3] = ds*vy;
		dQ[4] = ds*G;
		dQ[5] = ds*vz;

		return dQ;
	}
  
  
      
    public void ChangeVZ(){

    }
    
    public void ChangeVX(){

    
    }
}
