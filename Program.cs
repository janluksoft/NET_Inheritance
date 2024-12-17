// See https://aka.ms/new-console-template for more information
using System.Collections;
using System.Xml.Linq;

Console.WriteLine("Hello, We start with an application presenting multiple inheritance of classes in C#");
Console.WriteLine("Application in .NET9");
Console.WriteLine("Below is the calculated work W[J] of pulling an object with mass m [kg] on a surface");
Console.WriteLine("surface with friction (k - coefficient of kinetic friction). \r\n");

CWorkCondition cWorkCond = new CWorkCondition();
foreach (CWorkDone item in cWorkCond.colArr)
{
  Console.WriteLine(item.Message + " ");
}

Console.WriteLine("\r\n The End");
  

//====== END Main ========================
public class CGravityForce
{
  const double GravAcceleration = 9.81; //[m/s^2]
  public float Mass { get; protected set; } // body mass in kg
  public string Name { get; protected set; } //name of body
  public CGravityForce(float mass, string name)
  {
    this.Mass = mass;
    this.Name = name;
  }
  public float GravityForce()
  {
    float Fg = Mass * (float)GravAcceleration; //Fg = mg
    return (Fg);
  }
}

public class CFrictionForce : CGravityForce
{
  public string Surface { get; protected set; } //name of body
  public float KCFriction { get; protected set; }
  public CFrictionForce(float mass, string name, string surface) : base(mass, name)
  {
    CFrictionTable cFrictionT = new CFrictionTable();
    this.KCFriction = cFrictionT.GetCoefficientOfFriction(surface);

    //this.KCFriction = 0.25F; // kCFriction;
    this.Surface = surface;
  }

  public float FrictionForce()
  {
    float _mass = Mass; //it see the variable from inherited CGravityForce
    float Ff = KCFriction * GravityForce() ; //Fg = mg
    return (Ff);
  }

}

public class CFrictionTable
{
  SStringDouble[] FTable;
  public CFrictionTable()
  {
    FTable = new SStringDouble[] 
    { 
      new SStringDouble("Steel_on_Stell", 0.4),
      new SStringDouble("Steel_on_Ice"  , 0.05),
      new SStringDouble("Wood_on_Wood"  , 0.4),
      new SStringDouble("Wood_on_Snow"  , 0.06),
      new SStringDouble("Ice_on_Ice"    , 0.02),
      new SStringDouble("Glass_on_Glass", 0.35),
      new SStringDouble("Tire_on_Concrete", 0.8),
    };
  }

  //GetKineticCoefficientOfFriction()
  public float GetCoefficientOfFriction(string searchDesc)
  {
    foreach (var item in FTable)
    {
      if (item.Desc == searchDesc)
        return (float)item.Val;
    }
    return 0;
  }

  public double GetValOrZeroLinq(string searchDesc) =>
    FTable.FirstOrDefault(item => item.Desc == searchDesc)?.Val ?? 0;

  public class SStringDouble
  {
    public string Desc { get; protected set; }
    public double Val { get; protected set; }
    public SStringDouble(string desc, double val)
    {
      this.Desc = desc;
      this.Val = val;
    }
  }
}



public class CWorkDone : CFrictionForce
{
  public float Displacement { get; protected set; }
  public float WorkDone { get; protected set; }
  public string Message { get; protected set; }

  public CWorkDone(float mass, string name, string surface, float displacement)
                  : base(mass, name, surface)
  {
    this.Displacement = displacement;
    this.WorkDone = fWorkDone(); //End of result
    this.Message = "";
    fMakeMessage();
  }

  public float fWorkDone()
  {
    float Wd = Displacement * FrictionForce(); //Fg = mg
    return (Wd);
  }

  public void fMakeMessage()
  {
    this.Message = $" Item: {EquSt(Name,6)}" +
      $", surface: {EquSt(Surface, 16)} k= {KCFriction,4:0.00} " +
      $" m[kg]= {EquSt(Mass.ToString(), 3)}" +
      $" S[m]= {EquSt(Displacement.ToString(), 3)}End: W[J]= {WorkDone,6:0.000}"; // W[J]= {EquSt(WorkDone.ToString("0.000"), 12)}";
  }

  private string EquSt(string xmessage, int xLen)
  { //EqualString
    string mess = xmessage + "                       ";
    mess = mess.Substring(0, xLen);
    return (mess);
  }
}

public class CWorkCondition
{
  //CWorkDone cnWorkDone;// = new CWorkDone(10, "block", "Steel_on_Stell", 1);
  public ArrayList colArr;// = new ArrayList(); //Collection

  public CWorkCondition()
  {
    colArr = new ArrayList();

    colArr.Add(new CWorkDone(10, "block" , "Steel_on_Stell", 1)); // 10 kg, , , 1 m
    colArr.Add(new CWorkDone(10, "ice"   , "Ice_on_Ice"    , 1)); // 10 kg, , , 1 m
    colArr.Add(new CWorkDone(10, "box"   , "Wood_on_Wood"  , 1)); // 10 kg, , , 1 m
    colArr.Add(new CWorkDone(10, "glass" , "Glass_on_Glass", 1)); // 10 kg, , , 1 m
    colArr.Add(new CWorkDone(10, "ski"   , "Wood_on_Snow"  , 1)); // 10 kg, , , 1 m
    colArr.Add(new CWorkDone(10, "rubber", "Tire_on_Concrete", 1)); // 10 kg, , , 1 m
  }
}
