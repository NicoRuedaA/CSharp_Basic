using System;
using System.Windows.Forms;
using System.Drawing;
public class Keydemo:Form
{
public Keydemo()
{
this.KeyUp += new KeyEventHandler(OnKeyPress);
}
public void OnKeyPress(object sender, KeyEventArgs e)
{
MessageBox.Show(e.KeyCode.ToString(), "Your input");
}
public static void Main()
{
Application.Run(new Keydemo());
}
}