using System;

public class WindProjectile : DragProjectile 
{
  private double windVx;
  private double windVy;

  public WindProjectile(double x0, double y0, double z0, 
             double vx0, double vy0, double vz0, double time,
             double mass, double area, double density, double Cd,
             double windVx, double windVy) :
               base(x0, y0, z0, vx0, vy0, vz0, time, mass, 
                    area, density, Cd) {

    //  Initialize variables declared in the WindProjectile class.
    this.windVx = windVx;
    this.windVy = windVy;
  }

  //  These properties are used to access the fields declared
  //  in the class.
  public double WindVx {
    get {
      return windVx;
    }
  }

  public double WindVy {
    get {
      return windVy;
    }
  }
}