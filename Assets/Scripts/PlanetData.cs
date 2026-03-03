using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PlanetData
{
  public enum Planet { Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune }
  public enum KeplerParameter { a, e, I, L, longPeri, longNode, b, c, s, f}

  /// <summary>
  /// Get planet coordinates at a given time (in AU)
  /// </summary>
  /// <param name="p"></param>
  /// <param name="t"></param>
  /// <returns></returns>
  public static Vector3 GetPlanetPosition(Planet p, DateTime t)
  {
    float T = (float)(t-new DateTime(2000,1,1)).TotalDays/36525f;
    float a = GetKeplerParameter(p, KeplerParameter.a)[0] 
              + (GetKeplerParameter(p, KeplerParameter.a)[1] * T);
    float longPeri = GetKeplerParameter(p, KeplerParameter.longPeri)[0]
                     + (GetKeplerParameter(p, KeplerParameter.longPeri)[1] * T);
    float longNode = GetKeplerParameter(p, KeplerParameter.longNode)[0]
                     + (GetKeplerParameter(p, KeplerParameter.longNode)[1] * T);
    float peri = longPeri - longNode;
    float M = GetKeplerParameter(p, KeplerParameter.L)[0]
              + (GetKeplerParameter(p, KeplerParameter.L)[1] * T)
              - longPeri
              + (GetKeplerParameter(p, KeplerParameter.b)[0] * T * T)
              + GetKeplerParameter(p, KeplerParameter.c)[0] * Mathf.Cos(Mathf.Deg2Rad*GetKeplerParameter(p, KeplerParameter.f)[0] * T)
              + GetKeplerParameter(p, KeplerParameter.s)[0] * Mathf.Cos(Mathf.Deg2Rad*GetKeplerParameter(p, KeplerParameter.f)[0] * T);

    M = M % 360 - 180;

    float e = GetKeplerParameter(p, KeplerParameter.e)[0]
              + (GetKeplerParameter(p, KeplerParameter.e)[1] * T);
    float e_star = e * 180 / Mathf.PI;

    double E = M - (e_star * Mathf.Sin(Mathf.Deg2Rad*M));
    double deltaE;
    //double deltaM;
    int i = 0;
    do
    {
      //deltaM = M - E + (e_star * Mathf.Sin((float)(Mathf.Deg2Rad*E)));
      //deltaE = deltaM / (1 - (e_star * Mathf.Cos((float)(Mathf.Deg2Rad*E))));
      
      // Don't know why the serie do not converge with order 2 term (see above)
      // Using only order 1 term instead
      deltaE = M - E + (e_star * Mathf.Sin((float)(Mathf.Deg2Rad * E)));
      E += deltaE;
      i++;
    } while (Mathf.Abs((float)(deltaE)) > 0.000001 && i<100);

    float x_prime = a * (Mathf.Cos((float)(Mathf.Deg2Rad * E)) - e);
    float y_prime = a * Mathf.Sqrt(1 - e * e) * Mathf.Sin((float)(Mathf.Deg2Rad*E));
    
    float incl = GetKeplerParameter(p, KeplerParameter.I)[0]
                 + (GetKeplerParameter(p, KeplerParameter.I)[1] * T);

    return new Vector3((Mathf.Cos(Mathf.Deg2Rad * peri) * Mathf.Cos(Mathf.Deg2Rad * longNode) - Mathf.Sin(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * longNode) * Mathf.Cos(Mathf.Deg2Rad * incl)) * x_prime 
        + (-Mathf.Sin(Mathf.Deg2Rad * peri) * Mathf.Cos(Mathf.Deg2Rad * longNode) - Mathf.Cos(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * longNode) * Mathf.Cos(Mathf.Deg2Rad * incl)) * y_prime,
        (Mathf.Cos(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * longNode) + Mathf.Sin(Mathf.Deg2Rad * peri) * Mathf.Cos(Mathf.Deg2Rad * longNode) * Mathf.Cos(Mathf.Deg2Rad * incl)) * x_prime
        + (-Mathf.Sin(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * longNode) + Mathf.Cos(Mathf.Deg2Rad * peri) * Mathf.Cos(Mathf.Deg2Rad * longNode) * Mathf.Cos(Mathf.Deg2Rad * incl)) * y_prime,
        Mathf.Sin(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * incl) * x_prime
        + Mathf.Cos(Mathf.Deg2Rad * peri) * Mathf.Sin(Mathf.Deg2Rad * incl) * y_prime);
    }

  private static float[] GetKeplerParameter(Planet planet, KeplerParameter param)
  {
    return planet switch
    {
      Planet.Mercury => param switch
      {
        KeplerParameter.a => new float[] { 0.38709927f, 0 },
        KeplerParameter.e => new float[] { 0.20563661f, 0.00002123f },
        KeplerParameter.I => new float[] { 7.00559432f, -0.00590158f },
        KeplerParameter.L => new float[] { 252.25166724f, 149472.67486623f },
        KeplerParameter.longPeri => new float[] { 77.45771895f, 0.15940013f },
        KeplerParameter.longNode => new float[] { 48.33961819f, -0.12214182f },
        _ => new float[] { 0, 0 },
      },
      Planet.Venus => param switch
      {
        KeplerParameter.a => new float[] { 0.72332102f, -0.00000026f },
        KeplerParameter.e => new float[] { 0.00676399f, -0.00005107f },
        KeplerParameter.I => new float[] { 3.39777545f, 0.00043494f },
        KeplerParameter.L => new float[] { 181.97970850f, 58517.81560260f },
        KeplerParameter.longPeri => new float[] { 131.76755713f, 0.05679648f },
        KeplerParameter.longNode => new float[] { 76.67261496f, -0.27274174f },
        _ => new float[] { 0, 0 },
      },
      Planet.Earth => param switch
      {
        KeplerParameter.a => new float[] { 1.00000018f, -0.00000003f },
        KeplerParameter.e => new float[] { 0.01673163f, -0.00003661f },
        KeplerParameter.I => new float[] { -0.00054346f, -0.01337178f },
        KeplerParameter.L => new float[] { 100.46691572f, 35999.37306329f },
        KeplerParameter.longPeri => new float[] { 102.93005885f, 0.31795260f },
        KeplerParameter.longNode => new float[] { -5.11260389f, -0.24123856f },
        _ => new float[] { 0, 0 },
      },
      Planet.Mars => param switch
      {
        KeplerParameter.a => new float[] { 1.52371243f, 0.00000097f },
        KeplerParameter.e => new float[] { 0.09336511f, 0.00009149f },
        KeplerParameter.I => new float[] { 1.85181869f, -0.00724757f },
        KeplerParameter.L => new float[] { -4.56813164f, 19140.29934243f },
        KeplerParameter.longPeri => new float[] { -23.91744784f, 0.45223625f },
        KeplerParameter.longNode => new float[] { 49.71320984f, -0.26852431f },
        _ => new float[] { 0, 0 },
      },
      Planet.Jupiter => param switch
      {
        KeplerParameter.a => new float[] { 5.20248019f, -0.00002864f },
        KeplerParameter.e => new float[] { 0.04853590f, 0.00018026f },
        KeplerParameter.I => new float[] { 1.29861416f, -0.00322699f },
        KeplerParameter.L => new float[] { 34.33479152f, 3034.90371757f },
        KeplerParameter.longPeri => new float[] { 14.27495244f, 0.18199196f },
        KeplerParameter.longNode => new float[] { 100.29282654f, 0.13024619f },
        KeplerParameter.b => new float[] { -0.00012452f, 0 },
        KeplerParameter.c => new float[] { 0.06064060f, 0 },
        KeplerParameter.s => new float[] { -0.35635438f, 0 },
        KeplerParameter.f => new float[] { 38.35125000f, 0 },
        _ => new float[] { 0, 0 },
      },
      Planet.Saturn => param switch
      {
        KeplerParameter.a => new float[] { 9.54149883f, -0.00003065f },
        KeplerParameter.e => new float[] { 0.05550825f, -0.00032044f },
        KeplerParameter.I => new float[] { 2.49424102f, 0.00451969f },
        KeplerParameter.L => new float[] { 50.07571329f, 1222.11494724f },
        KeplerParameter.longPeri => new float[] { 92.86136063f, 0.54179478f },
        KeplerParameter.longNode => new float[] { 113.63998702f, -0.25015002f },
        KeplerParameter.b => new float[] { 0.00025899f, 0 },
        KeplerParameter.c => new float[] { -0.13434469f, 0 },
        KeplerParameter.s => new float[] { 0.87320147f, 0 },
        KeplerParameter.f => new float[] { 38.35125000f, 0 },
        _ => new float[] { 0, 0 },
      },
      Planet.Uranus => param switch
      {
        KeplerParameter.a => new float[] { 19.18797948f, -0.00020455f },
        KeplerParameter.e => new float[] { 0.04685740f, -0.00001550f },
        KeplerParameter.I => new float[] { 0.77298127f, -0.00180155f },
        KeplerParameter.L => new float[] { 314.20276625f, 428.49512595f },
        KeplerParameter.longPeri => new float[] { 172.43404441f, 0.09266985f },
        KeplerParameter.longNode => new float[] { 73.96250215f, 0.05739699f },
        KeplerParameter.b => new float[] { 0.00058331f, 0 },
        KeplerParameter.c => new float[] { -0.97731848f, 0 },
        KeplerParameter.s => new float[] { 0.17689245f, 0 },
        KeplerParameter.f => new float[] { 7.67025000f, 0 },
        _ => new float[] { 0, 0 },
      },
      Planet.Neptune => param switch
      {
        KeplerParameter.a => new float[] { 30.06952752f, 0.00006447f },
        KeplerParameter.e => new float[] { 0.00895439f, 0.00000818f },
        KeplerParameter.I => new float[] { 1.77005520f, 0.00022400f },
        KeplerParameter.L => new float[] { 304.22289287f, 218.46515314f },
        KeplerParameter.longPeri => new float[] { 46.68158724f, 0.01009938f },
        KeplerParameter.longNode => new float[] { 131.78635853f, -0.00606302f },
        KeplerParameter.b => new float[] { -0.00041348f, 0 },
        KeplerParameter.c => new float[] { 0.68346318f, 0 },
        KeplerParameter.s => new float[] { -0.10162547f, 0 },
        KeplerParameter.f => new float[] { 7.67025000f, 0 },
        _ => new float[] { 0, 0 },
      },
      _ => new float[] { 0, 0 },
    };
  }
}