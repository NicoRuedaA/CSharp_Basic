using System;

public class DragProjectile : SimpleProjectile
{
  private double mass;
  private double area;
  private double density;
  private double cd;

  public DragProjectile(double x0, double y0, double z0, 
             double vx0, double vy0, double vz0, double time,
             double mass, double area, double density, double cd) :
             base(x0, y0, z0, vx0, vy0, vz0, time) {

    //  Initialize variables declared in the DragProjectile class.
    this.mass = mass;
    this.area = area;
    this.density = density;
    this.cd = cd;
  }

  //  These properties are used to access the fields declared
  //  in the class.
  public double Mass {
    get {
      return mass;
    }
  }

  public double Area {
    get {
      return area;
    }
  }

  public double Density {
    get {
      return density;
    }
  }

  public double Cd {
    get {
      return cd;
    }
  }
}