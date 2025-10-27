using System;

public class ODESolver
{
  //  Fourth-order Runge-Kutta ODE solver.  
  public static void RungeKutta4(ODE ode, double ds) {


  		/*
		double x0, z0;
        double vx0, vz0;
        double time0;

        x0 = this.posX;
        z0 = this.posZ;
        vx0 = this.velX;
        vz0 = this.velZ;
        time0 = this.timeTotal;

        double time;
        time = time0+dt;

        double vx,x;
        vx = vx0;
        x = x0+vx0*dt;
        double vz, z;
        vz = vz0;
        z = z0 + vz0 * dt;


        this.posX = x;
        this.posZ = z;
        this.velX = vx;
        this.velZ = vz;*/
        //this.timeTotal = time;
  	
  
  	
    int j;
    int numEqns = ode.NumEqns;
    double s;
    double[] q;
    double[] dq1 = new double[numEqns];
    double[] dq2 = new double[numEqns];
    double[] dq3 = new double[numEqns];
    double[] dq4 = new double[numEqns];

    s = ode.S;
    q = ode.GetAllQ();

    dq1 = ode.GetRightHandSide(s, q, q, ds, 0.0);
    dq2 = ode.GetRightHandSide(s+0.5*ds, q, dq1, ds, 0.5);
    dq3 = ode.GetRightHandSide(s+0.5*ds, q, dq2, ds, 0.5);
    dq4 = ode.GetRightHandSide(s+ds, q, dq3, ds, 1.0);


    ode.S = s + ds;

    for(j=0; j<numEqns; ++j) {
      q[j] = q[j] + (dq1[j] + 2.0*dq2[j] + 2.0*dq3[j] + dq4[j])/6.0;
      ode.SetQ(q[j], j);
    }     

    return;
  }
  
  public void ChangeVZ(){
    //return 0	;
  	//this.velZ=-this.velZ;
    }
    
    public void ChangeVX(){
  	//return 0;
    	//this.velX=-this.velX;
    }
}
  
  

