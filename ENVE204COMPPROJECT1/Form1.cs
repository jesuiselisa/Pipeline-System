using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENVE204COMPPROJECT1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Made by Elişa UÇMAK 

        int pipenumber;
        string material;
        double temperature, v, hL, D1, D2, D3, L1, L2, L3, f1, f2, f3, V1, V2, V3, Q, g;
        //v: kinematic viscosity, hL: head loss, L: length, D: diameter, Q: flow rate, f: friction factor, V: velocity, g: Gravity Acceleration, A: alan
        double roughnessheight, A1, A2, A3, R1, R2, R3, Rtotal, Re1, Re2, Re3, fold, fnew, fnew1, fnew2, fnew3, fnet;
        //NOTE: Actually I want to use "e" for roughness heigth but when I use this name, system will understand e number which use in math. Therefore I can't use it.

        private void cmbxPipeNumber_onItemSelected(object sender, EventArgs e)
        {
            if (cmbxPipeNumber.selectedIndex == 0)
            {
                lblDia1.Visible = true;
                txtDia1.Visible = true;
                lblFriction1.Visible = true;
                txtFriction1.Visible = true;
                lblLength1.Visible = true;
                txtLength1.Visible = true;
                lblVelocity1.Visible = true;
                txtVelocity1.Visible = true;

                lblDia2.Visible = false;
                txtDia2.Visible = false;
                lblFriction2.Visible = false;
                txtFriction2.Visible = false;
                lblLength2.Visible = false;
                txtLength2.Visible = false;
                lblVelocity2.Visible = false;
                txtVelocity2.Visible = false;

                lblDia3.Visible = false;
                txtDia3.Visible = false;
                lblFriction3.Visible = false;
                txtFriction3.Visible = false;
                lblLength3.Visible = false;
                txtLength3.Visible = false;
                lblVelocity3.Visible = false;
                txtVelocity3.Visible = false;

            }
            else if (cmbxPipeNumber.selectedIndex == 1)
            {
                lblDia1.Visible = true;
                txtDia1.Visible = true;
                lblFriction1.Visible = true;
                txtFriction1.Visible = true;
                lblLength1.Visible = true;
                txtLength1.Visible = true;
                lblVelocity1.Visible = true;
                txtVelocity1.Visible = true;

                lblDia2.Visible = true;
                txtDia2.Visible = true;
                lblFriction2.Visible = true;
                txtFriction2.Visible = true;
                lblLength2.Visible = true;
                txtLength2.Visible = true;
                lblVelocity2.Visible = true;
                txtVelocity2.Visible = true;

                lblDia3.Visible = false;
                txtDia3.Visible = false;
                lblFriction3.Visible = false;
                txtFriction3.Visible = false;
                lblLength3.Visible = false;
                txtLength3.Visible = false;
                lblVelocity3.Visible = false;
                txtVelocity3.Visible = false;
            }
            else
            {
                lblDia1.Visible = true;
                txtDia1.Visible = true;
                lblFriction1.Visible = true;
                txtFriction1.Visible = true;
                lblLength1.Visible = true;
                txtLength1.Visible = true;
                lblVelocity1.Visible = true;
                txtVelocity1.Visible = true;

                lblDia2.Visible = true;
                txtDia2.Visible = true;
                lblFriction2.Visible = true;
                txtFriction2.Visible = true;
                lblLength2.Visible = true;
                txtLength2.Visible = true;
                lblVelocity2.Visible = true;
                txtVelocity2.Visible = true;

                lblDia3.Visible = true;
                txtDia3.Visible = true;
                lblFriction3.Visible = true;
                txtFriction3.Visible = true;
                lblLength3.Visible = true;
                txtLength3.Visible = true;
                lblVelocity3.Visible = true;
                txtVelocity3.Visible = true;
            }
        }

        private void btnCalculateT3_Click(object sender, EventArgs e)
        {
            if (cmbxPipeMaterial.selectedIndex != -1 && cmbxPipeNumber.selectedIndex != -1 && txtHeadLoss.Text != null && txtTemperature.Text != null && txtFlowRate.Text != null)
            {
                material = cmbxPipeMaterial.selectedValue.ToString();
                pipenumber = Convert.ToInt32(cmbxPipeNumber.selectedValue);

                if (radioGroup1.SelectedIndex == 0)
                {
                    //In this calculations we use SI System beacuse of the choosing "SI Unit System"

                    g = 9.81; //g: m/s^2

                    //roughness height unit : mm
                    if (material == "Brass" || material == "Copper" || material == "HDPE" || material == "PVC")
                    {
                        roughnessheight = 0.0015;
                    }
                    else if (material == "Cast Iron")
                    {
                        roughnessheight = 0.26;
                    }
                    else if (material == "CMP")
                    {
                        roughnessheight = 45;
                    }
                    else if (material == "Commercial Steel")
                    {
                        roughnessheight = 0.045;
                    }
                    else if (material == "Galvanized Iron")
                    {
                        roughnessheight = 0.15;
                    }
                    else if (material == " Rough Concrete")
                    {
                        roughnessheight = 0.60;
                    }
                    else if (material == "Seamless Steel")
                    {
                        roughnessheight = 0.004;
                    }
                    else
                    {
                        roughnessheight = 0.18;
                    }

                    //For roughness height mm to m : 
                    roughnessheight = roughnessheight * Math.Pow(10, (-3));
                    //Now roughness height: m , I transfered m because diameter is in m.
                    hL = Convert.ToDouble(txtHeadLoss.Text);
                    temperature = Convert.ToDouble(txtTemperature.Text);
                    Q = Convert.ToDouble((txtFlowRate.Text));

                    //v: m^2/sec
                    if (temperature == 0)
                    {
                        v = 1.785 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 5)
                    {
                        v = 1.519 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 10)
                    {
                        v = 1.306 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 15)
                    {
                        v = 1.139 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 20)
                    {
                        v = 1.003 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 25)
                    {
                        v = 0.893 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 30)
                    {
                        v = 0.800 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 40)
                    {
                        v = 0.658 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 50)
                    {
                        v = 0.553 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 60)
                    {
                        v = 0.474 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 70)
                    {
                        v = 0.413 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 80)
                    {
                        v = 0.364 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 90)
                    {
                        v = 0.326 * Math.Pow(10, (-6));
                    }
                    else
                    {
                        v = 0.294 * Math.Pow(10, (-6));
                    }


                    if (pipenumber == 1)
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        Re1 = V1 * D1 / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fold = f1;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }
                    else if (pipenumber == 2)
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        f2 = Convert.ToDouble(txtFriction2.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew = (fnew1 + fnew2) / 2;
                        fold = (f1 + f2) / 2;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }
                    else
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        f2 = Convert.ToDouble(txtFriction2.Text);
                        f3 = Convert.ToDouble(txtFriction3.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        L3 = Convert.ToDouble(txtLength3.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D3 = Math.Pow((16 * f3 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                        V3 = (4 * Q) / (Math.PI * Math.Pow(D3, 2));
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        Re3 = (V3 * D3) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                        fnew = (fnew1 + fnew2 + fnew3) / 3;
                        fold = (f1 + f2 + f3) / 3;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }

                    //Continuation of Steps
                    if (fnet < 0.001)
                    {
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtDia1.Text = D1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtDia2.Text = D2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                        txtDia3.Text = D3.ToString();
                    }
                    else
                    {
                        while (fnet > 0.001)
                        {
                            if (pipenumber == 1)
                            {
                                f1 = fnew1;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                Re1 = (V1 * D1) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew = fnew1;
                                fold = f1;
                                fnet = Math.Abs(fnew - fold);
                            }
                            else if (pipenumber == 2)
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew = (fnew1 + fnew2) / 2;
                                fold = (f1 + f2) / 2;
                                fnet = Math.Abs(fnew - fold);
                            }
                            else
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                f3 = fnew3;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D3 = Math.Pow((16 * f3 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                                V3 = (4 * Q) / (Math.PI * Math.Pow(D3, 2));
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                Re3 = (V3 * D3) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                                fnew = (fnew1 + fnew2 + fnew3) / 3;
                                fold = (f1 + f2 + f3) / 3;
                                fnet = Math.Abs(fnew - fold);
                            }
                        }
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtDia1.Text = D1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtDia2.Text = D2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                        txtDia3.Text = D3.ToString();
                    }

                    //Finish

                }
                else
                {
                    //In this calculations we use British System beacuse of the choosing "British Unit System"

                    double g = 32.17; //g: gravity ft/s^2

                    //roughness height unit : ft
                    if (material == "Brass" || material == "Copper" || material == "HDPE" || material == "PVC")
                    {
                        roughnessheight = 0.000005;
                    }
                    else if (material == "Cast Iron")
                    {
                        roughnessheight = 0.00085;
                    }
                    else if (material == "CMP")
                    {
                        roughnessheight = 0.15;
                    }
                    else if (material == "Commercial Steel")
                    {
                        roughnessheight = 0.00015;
                    }
                    else if (material == "Galvanized Iron")
                    {
                        roughnessheight = 0.0005;
                    }
                    else if (material == " Rough Concrete")
                    {
                        roughnessheight = 0.002;
                    }
                    else if (material == "Seamless Steel")
                    {
                        roughnessheight = 0.000013;
                    }
                    else
                    {
                        roughnessheight = 0.0006;
                    }

                    hL = Convert.ToDouble(txtHeadLoss.Text);
                    temperature = Convert.ToDouble(txtTemperature.Text);

                    //v: ft^2/sec
                    if (temperature == 32)
                    {
                        v = 1.924 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 40)
                    {
                        v = 1.664 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 50)
                    {
                        v = 1.407 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 60)
                    {
                        v = 1.210 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 70)
                    {
                        v = 1.052 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 80)
                    {
                        v = 0.926 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 90)
                    {
                        v = 0.823 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 100)
                    {
                        v = 0.738 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 120)
                    {
                        v = 0.607 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 140)
                    {
                        v = 0.511 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 160)
                    {
                        v = 0.439 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 180)
                    {
                        v = 0.383 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 200)
                    {
                        v = 0.339 * Math.Pow(10, -5);
                    }
                    else
                    {
                        v = 0.317 * Math.Pow(10, -5);
                    }


                    if (pipenumber == 1)
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        Re1 = V1 * D1 / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fold = f1;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }
                    else if (pipenumber == 2)
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        f2 = Convert.ToDouble(txtFriction2.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew = (fnew1 + fnew2) / 2;
                        fold = (f1 + f2) / 2;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }
                    else
                    {
                        f1 = Convert.ToDouble(txtFriction1.Text);
                        f2 = Convert.ToDouble(txtFriction2.Text);
                        f3 = Convert.ToDouble(txtFriction3.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        L3 = Convert.ToDouble(txtLength3.Text);
                        //A = (PI * D^2)/4 : so I use this equation rather than write A
                        //hL = (f*L*Q^2)/(2*g*D*A^2) when we leave alone the diameter:
                        D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        D3 = Math.Pow((16 * f3 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                        V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                        V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                        V3 = (4 * Q) / (Math.PI * Math.Pow(D3, 2));
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        Re3 = (V3 * D3) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                        fnew = (fnew1 + fnew2 + fnew3) / 3;
                        fold = (f1 + f2 + f3) / 3;
                        fnet = Math.Abs(fnew - fold);
                        //After that calculations, continuation is below the if operation
                    }

                    //Continuation of Steps
                    if (fnet < 0.001)
                    {
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtDia1.Text = D1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtDia2.Text = D2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                        txtDia3.Text = D3.ToString();
                    }
                    else
                    {
                        while (fnet > 0.001)
                        {
                            if (pipenumber == 1)
                            {
                                f1 = fnew1;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                Re1 = (V1 * D1) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew = fnew1;
                                fold = f1;
                                fnet = Math.Abs(fnew - fold);
                            }
                            else if (pipenumber == 2)
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew = (fnew1 + fnew2) / 2;
                                fold = (f1 + f2) / 2;
                                fnet = Math.Abs(fnew - fold);
                            }
                            else
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                f3 = fnew3;
                                D1 = Math.Pow((16 * f1 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D2 = Math.Pow((16 * f2 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                D3 = Math.Pow((16 * f3 * Math.Pow(Q, 2)) / (2 * g * hL * Math.Pow(Math.PI, 2)), (1 / 5));
                                V1 = (4 * Q) / (Math.PI * Math.Pow(D1, 2));
                                V2 = (4 * Q) / (Math.PI * Math.Pow(D2, 2));
                                V3 = (4 * Q) / (Math.PI * Math.Pow(D3, 2));
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                Re3 = (V3 * D3) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                                fnew = (fnew1 + fnew2 + fnew3) / 3;
                                fold = (f1 + f2 + f3) / 3;
                                fnet = Math.Abs(fnew - fold);
                            }
                        }
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtDia1.Text = D1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtDia2.Text = D2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                        txtDia3.Text = D3.ToString();
                    }

                }

                //Finish

            }
            else
            {
                MessageBox.Show("ERROR! Please fill in all necessary information completely.");
            }

        }

        private void btnCalculateT2_Click(object sender, EventArgs e)
        {
            if (cmbxPipeMaterial.selectedIndex != -1 && cmbxPipeNumber.selectedIndex != -1 && txtHeadLoss.Text != null && txtTemperature.Text != null)
            {
                material = cmbxPipeMaterial.selectedValue.ToString();
                pipenumber = Convert.ToInt32(cmbxPipeNumber.selectedValue);

                if (radioGroup1.SelectedIndex == 0)
                {
                    //In this calculations we use SI System beacuse of the choosing "SI Unit System"

                    g = 9.81; //g: m/s^2

                    //roughness height unit : mm
                    if (material == "Brass" || material == "Copper" || material == "HDPE" || material == "PVC")
                    {
                        roughnessheight = 0.0015;
                    }
                    else if (material == "Cast Iron")
                    {
                        roughnessheight = 0.26;
                    }
                    else if (material == "CMP")
                    {
                        roughnessheight = 45;
                    }
                    else if (material == "Commercial Steel")
                    {
                        roughnessheight = 0.045;
                    }
                    else if (material == "Galvanized Iron")
                    {
                        roughnessheight = 0.15;
                    }
                    else if (material == " Rough Concrete")
                    {
                        roughnessheight = 0.60;
                    }
                    else if (material == "Seamless Steel")
                    {
                        roughnessheight = 0.004;
                    }
                    else
                    {
                        roughnessheight = 0.18;
                    }

                    //For roughness height mm to m : 
                    roughnessheight = roughnessheight * Math.Pow(10, (-3));
                    //Now roughness height: m , I transfered m because diameter is in m.
                    hL = Convert.ToDouble(txtHeadLoss.Text);
                    temperature = Convert.ToDouble(txtTemperature.Text);

                    //v: m^2/sec
                    if (temperature == 0)
                    {
                        v = 1.785 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 5)
                    {
                        v = 1.519 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 10)
                    {
                        v = 1.306 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 15)
                    {
                        v = 1.139 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 20)
                    {
                        v = 1.003 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 25)
                    {
                        v = 0.893 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 30)
                    {
                        v = 0.800 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 40)
                    {
                        v = 0.658 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 50)
                    {
                        v = 0.553 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 60)
                    {
                        v = 0.474 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 70)
                    {
                        v = 0.413 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 80)
                    {
                        v = 0.364 * Math.Pow(10, (-6));
                    }
                    else if (temperature <= 90)
                    {
                        v = 0.326 * Math.Pow(10, (-6));
                    }
                    else
                    {
                        v = 0.294 * Math.Pow(10, (-6));
                    }

                    //In Step-1, I don't use 5.74/(Re^0.9) in formula of friction factor because Re number is so big, when we start.
                    if (pipenumber == 1)
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        Rtotal = R1;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        Re1 = (V1 * D1) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew = fnew1;
                        //Step-5 
                        fold = f1;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }
                    else if (pipenumber == 2)
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        D2 = Convert.ToDouble(txtDia2.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        A2 = (Math.PI * Math.Pow(D2, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        f2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                        Rtotal = R1 + R2;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        V2 = Q / A2;
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew = (fnew1 + fnew2) / 2;
                        //Step-5 
                        fold = (f1 + f2) / 2;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }
                    else
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        D2 = Convert.ToDouble(txtDia2.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        D3 = Convert.ToDouble(txtDia3.Text);
                        L3 = Convert.ToDouble(txtLength3.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        A2 = (Math.PI * Math.Pow(D2, 2)) / 4;
                        A3 = (Math.PI * Math.Pow(D3, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        f2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7)), 2);
                        f3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                        R3 = (f3 * L3) / (2 * g * D3 * Math.Pow(A3, 2));
                        Rtotal = R1 + R2 + R3;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        V2 = Q / A2;
                        V3 = Q / A3;
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        Re3 = (V3 * D3) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                        fnew = (fnew1 + fnew2 + fnew3) / 3;
                        //Step-5 
                        fold = (f1 + f2 + f3) / 3;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }

                    //Continuation of Step-5
                    if (fnet < 0.001)
                    {
                        txtFlowRate.Text = Q.ToString();
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                    }
                    else
                    {
                        while (fnet > 0.001)
                        {
                            if (pipenumber == 1)
                            {
                                f1 = fnew1;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                Rtotal = R1;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                Re1 = (V1 * D1) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew = fnew1;
                                fold = f1;
                                fnet = fnew - fold;
                            }
                            else if (pipenumber == 2)
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                                Rtotal = R1 + R2;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                V2 = Q / A2;
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew = (fnew1 + fnew2) / 2;
                                fold = (f1 + f2) / 2;
                                fnet = fnew - fold;
                            }
                            else
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                f3 = fnew3;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                                R3 = (f3 * L3) / (2 * g * D3 * Math.Pow(A3, 2));
                                Rtotal = R1 + R2 + R3;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                V2 = Q / A2;
                                V3 = Q / A3;
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                Re3 = (V3 * D3) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                                fnew = (fnew1 + fnew2 + fnew3) / 3;
                                fold = (f1 + f2 + f3) / 3;
                                fnet = fnew - fold;
                            }
                        }
                        txtFlowRate.Text = Q.ToString();
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                    }

                    //Finish

                }
                else
                {
                    //In this calculations we use British System beacuse of the choosing "British Unit System"

                    double g = 32.17; //g: gravity ft/s^2

                    //roughness height unit : ft
                    if (material == "Brass" || material == "Copper" || material == "HDPE" || material == "PVC")
                    {
                        roughnessheight = 0.000005;
                    }
                    else if (material == "Cast Iron")
                    {
                        roughnessheight = 0.00085;
                    }
                    else if (material == "CMP")
                    {
                        roughnessheight = 0.15;
                    }
                    else if (material == "Commercial Steel")
                    {
                        roughnessheight = 0.00015;
                    }
                    else if (material == "Galvanized Iron")
                    {
                        roughnessheight = 0.0005;
                    }
                    else if (material == " Rough Concrete")
                    {
                        roughnessheight = 0.002;
                    }
                    else if (material == "Seamless Steel")
                    {
                        roughnessheight = 0.000013;
                    }
                    else
                    {
                        roughnessheight = 0.0006;
                    }

                    hL = Convert.ToDouble(txtHeadLoss.Text);
                    temperature = Convert.ToDouble(txtTemperature.Text);

                    //v: ft^2/sec
                    if (temperature == 32)
                    {
                        v = 1.924 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 40)
                    {
                        v = 1.664 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 50)
                    {
                        v = 1.407 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 60)
                    {
                        v = 1.210 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 70)
                    {
                        v = 1.052 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 80)
                    {
                        v = 0.926 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 90)
                    {
                        v = 0.823 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 100)
                    {
                        v = 0.738 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 120)
                    {
                        v = 0.607 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 140)
                    {
                        v = 0.511 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 160)
                    {
                        v = 0.439 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 180)
                    {
                        v = 0.383 * Math.Pow(10, -5);
                    }
                    else if (temperature <= 200)
                    {
                        v = 0.339 * Math.Pow(10, -5);
                    }
                    else
                    {
                        v = 0.317 * Math.Pow(10, -5);
                    }

                    //In Step-1, I don't use 5.74/(Re^0.9) in formula of friction factor because Re number is so big, when we start.
                    if (pipenumber == 1)
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        Rtotal = R1;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        Re1 = (V1 * D1) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew = fnew1;
                        //Step-5 
                        fold = f1;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }
                    else if (pipenumber == 2)
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        D2 = Convert.ToDouble(txtDia2.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        A2 = (Math.PI * Math.Pow(D2, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        f2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                        Rtotal = R1 + R2;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        V2 = Q / A2;
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew = (fnew1 + fnew2) / 2;
                        //Step-5 
                        fold = (f1 + f2) / 2;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }
                    else
                    {
                        D1 = Convert.ToDouble(txtDia1.Text);
                        L1 = Convert.ToDouble(txtLength1.Text);
                        D2 = Convert.ToDouble(txtDia2.Text);
                        L2 = Convert.ToDouble(txtLength2.Text);
                        D3 = Convert.ToDouble(txtDia3.Text);
                        L3 = Convert.ToDouble(txtLength3.Text);
                        A1 = (Math.PI * Math.Pow(D1, 2)) / 4;
                        A2 = (Math.PI * Math.Pow(D2, 2)) / 4;
                        A3 = (Math.PI * Math.Pow(D3, 2)) / 4;
                        //Step-1
                        f1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7)), 2);
                        f2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7)), 2);
                        f3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7)), 2);
                        //Step-2
                        R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                        R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                        R3 = (f3 * L3) / (2 * g * D3 * Math.Pow(A3, 2));
                        Rtotal = R1 + R2 + R3;
                        //Step-3
                        Q = Math.Sqrt(hL / Rtotal);
                        //Step-4
                        V1 = Q / A1;
                        V2 = Q / A2;
                        V3 = Q / A3;
                        Re1 = (V1 * D1) / v;
                        Re2 = (V2 * D2) / v;
                        Re3 = (V3 * D3) / v;
                        fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                        fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                        fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                        fnew = (fnew1 + fnew2 + fnew3) / 3;
                        //Step-5 
                        fold = (f1 + f2 + f3) / 3;
                        fnet = fnew - fold;
                        //After that calculations, continuation of Step-5 is below the if operation
                    }

                    //Continuation of Step-5
                    if (fnet < 0.001)
                    {
                        txtFlowRate.Text = Q.ToString();
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                    }
                    else
                    {
                        while (fnet > 0.001)
                        {
                            if (pipenumber == 1)
                            {
                                f1 = fnew1;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                Rtotal = R1;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                Re1 = (V1 * D1) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew = fnew1;
                                fold = f1;
                                fnet = fnew - fold;
                            }
                            else if (pipenumber == 2)
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                                Rtotal = R1 + R2;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                V2 = Q / A2;
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew = (fnew1 + fnew2) / 2;
                                fold = (f1 + f2) / 2;
                                fnet = fnew - fold;
                            }
                            else
                            {
                                f1 = fnew1;
                                f2 = fnew2;
                                f3 = fnew3;
                                //Go to Step-2
                                R1 = (f1 * L1) / (2 * g * D1 * Math.Pow(A1, 2));
                                R2 = (f2 * L2) / (2 * g * D2 * Math.Pow(A2, 2));
                                R3 = (f3 * L3) / (2 * g * D3 * Math.Pow(A3, 2));
                                Rtotal = R1 + R2 + R3;
                                Q = Math.Sqrt(hL / Rtotal);
                                V1 = Q / A1;
                                V2 = Q / A2;
                                V3 = Q / A3;
                                Re1 = (V1 * D1) / v;
                                Re2 = (V2 * D2) / v;
                                Re3 = (V3 * D3) / v;
                                fnew1 = 1.325 / Math.Pow((Math.Log((roughnessheight / D1) / 3.7) + (5.74 / Math.Pow(Re1, 0.9))), 2);
                                fnew2 = 1.325 / Math.Pow((Math.Log((roughnessheight / D2) / 3.7) + (5.74 / Math.Pow(Re2, 0.9))), 2);
                                fnew3 = 1.325 / Math.Pow((Math.Log((roughnessheight / D3) / 3.7) + (5.74 / Math.Pow(Re3, 0.9))), 2);
                                fnew = (fnew1 + fnew2 + fnew3) / 3;
                                fold = (f1 + f2 + f3) / 3;
                                fnet = fnew - fold;
                            }
                        }
                        txtFlowRate.Text = Q.ToString();
                        txtFriction1.Text = fnew1.ToString();
                        txtVelocity1.Text = V1.ToString();
                        txtFriction2.Text = fnew2.ToString();
                        txtVelocity2.Text = V2.ToString();
                        txtFriction3.Text = fnew3.ToString();
                        txtVelocity3.Text = V3.ToString();
                    }

                    //Finish

                }

            }
            else
            {
                MessageBox.Show("ERROR! Please fill in all necessary information completely.");
            }

        }
    }
}
