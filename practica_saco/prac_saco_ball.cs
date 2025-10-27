using System;
using System.Diagnostics;

public class bagBall
{
    //  gravitational acceleration.
    public const double G = 0;
    double posX, posZ;
    double velX, velZ, vel;
    double timeTotal;

    //  Load the initial position, velocity, and time 
    public bagBall(double x0, double z0, double v, double time)
	{
        this.posX = x0;
        this.posZ = z0;
        this.vel = v;
        //this.velX = vx0;
        //this.velZ = vz0;
        this.timeTotal = time;
	}

    //  These methods return the location, velocity, 
    //  and time values
    public double GetVx()
    {
        return this.velX;
    }

    public double GetVz()
    {
        return this.velZ;
    }

    public double GetX()
    {
        return this.posX;
    }

    public double GetZ()
    {
        return this.posZ;
    }


    public double GetTime()
    {
        return this.timeTotal;
    }

    //  This method updates the velocity and position
    //  of the projectile according to the gravity-only model.
    public void UpdateLocationAndVelocity(double dt, double alpha, double velocidad, double g)
    {
	    double x0, z0;
	    double vx0, vz0;
		double x;    
	    double vz, vx,z;
	    
	    //Get values
	    x0 = this.posX;
	    z0 = this.posZ;
	    vx0 = this.velX;
	    vz0 = this.velZ;
	    timeTotal+=dt;
	    
	     
		double cos = Math.Cos(alpha * (Math.PI / 180.0));
		double sin = Math.Sin(alpha * (Math.PI / 180.0));
		
		vx= velocidad*cos;
		vz = velocidad*sin;
	    x= x0+vx0*cos*timeTotal;
	    z = z0+vz*sin*timeTotal-(g/2)*timeTotal*timeTotal;
	
	    // keep results
	    this.posX = x;
	    this.posZ = z;
	    this.velX = vx;
	    this.velZ = vz;
       
    }
}
