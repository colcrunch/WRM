using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Use array and lists rather than string conversion
            string[] shaped = new string[] { A1.Text, B1.Text, C1.Text, A2.Text, B2.Text, C2.Text, A3.Text, B3.Text, C3.Text };
            List<string> row1 = new List<string>();
            List<string> row2 = new List<string>();
            List<string> row3 = new List<string>();
            //Iterate over cells from row one
            for (int i=0; i <= 2; i++)
            {
                if (shaped[i].Length == 0)
                {
                    //If cell is empty set to "null" and add to list "row1"
                    row1.Add("null");
                }
                else
                {
                    //Otherwise add the triangle brackets and add to list "row1"
                    row1.Add("<" + shaped[i] + ">");
                }
            }
            //Join the list into a string.
            string row1_s = string.Join(", ", row1);

            //Iterate over array cells from row2
            for (int i = 3; i <= 5; i++)
            {
                if (shaped[i].Length == 0)
                {
                    //If the cell is empty set to null and add to list row2
                    row2.Add("null");
                }
                else
                {
                    //Otherwise add the triangle brackets and add to list row2
                    row2.Add("<" + shaped[i] + ">'");
                }
            }
            //Join the list to a string.
            string row2_s = string.Join(", ", row2);

            //Iterate over array sells from row3
            for (int i = 6; i <= 8; i++)
            {
                if (shaped[i].Length == 0)
                {
                    //If the cell is empty set to null and ad dot list row3
                    row3.Add("null");
                }
                else
                {
                    //Otherwise add the triangle brackets and add to list row3
                    row3.Add("<" + shaped[i] + ">");
                }
            }
            //Join the list to a string
            string row3_s = string.Join(", ", row3);

            if (string.IsNullOrEmpty(Output.Text))
            {
                MessageBox.Show("Please set an output for this recipe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                mto.Text = "recipes.addShaped(< " + Output.Text + " > *" + amount.Value + " [[" + row1_s + "], [" + row2_s + "], [" + row3_s + "]]);";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Copy the minetweaker output to the clipboard
            Clipboard.SetText(mto.Text);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show the About Box
            Form Form2 = new Form2();

            Form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Set the A Column to empty
            A1.Text = "";
            A2.Text = "";
            A3.Text = "";

            //Set the B Column to empty
            B1.Text = "";
            B2.Text = "";
            B3.Text = "";

            //Set the C Column to empty
            C1.Text = "";
            C2.Text = "";
            C3.Text = "";

            //Reset other fields
            Output.Text = "";
            amount.Value = 1;

            //Reset mto
            mto.Text = "";
        }

        private void gen_furn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(furn_in.Text) && !string.IsNullOrWhiteSpace(furn_out.Text))
            {
                if (XP.Value > 0)
                {
                    //If there is a real XP value, update the minetweaker output accordingly
                    this.mto_f.Text = "furnace.addrecipe(<" + furn_out.Text + ">, <" + furn_in.Text + ">, "+ XP.Value +");";
                }
                else
                {
                    //If the XP value is 0, update the minetweaker output accordingly.
                    this.mto_f.Text = "furnace.addrecipe(<" + furn_out.Text + ">, <" + furn_in.Text + ">);";
                }
            }
            else
            {
                //Warn user if neither of the required boxes are filled out
                MessageBox.Show("You must fill in the input (bottom) and output (top) boxes! (XP is optional)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clear_furn_Click(object sender, EventArgs e)
        { 
            //Reset minetweaker output
            this.mto_f.Text = "";

            //Reset Recipe
            furn_in.Text = "";
            furn_out.Text = "";
            XP.Value = 0;
        }

        private void gen_fuel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(fuel_i.Text) && burn_time.Value > 0)
            {
                //If both boxes are filled out, update the minetweaker output
                this.mto_f.Text = "furnace.setFuel(<" + fuel_i.Text + ">, " + burn_time.Value + ");";
            }
            else if(!string.IsNullOrWhiteSpace(fuel_i.Text) && burn_time.Value == 0)
            {
                // If burn time is 0, warn user about the implications
               DialogResult result1 = MessageBox.Show("If you set burn time to 0 you will remove this item as a fuel!", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if(result1 == DialogResult.OK)
                {
                    //If user selects "OK", update the minetweaker output
                    this.mto_f.Text = "furnace.setFuel(<" + fuel_i.Text + ">, " + burn_time.Value + ");";
                }
            }
            else
            {
                //If neither box is filled out, alert the user
                MessageBox.Show("Please fill in the fuel box and the burn time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clear_fuel_Click(object sender, EventArgs e)
        {
            //Reset minetweaker output
            this.mto_f.Text = "";

            //Reset inputs in the fuel box
            this.fuel_i.Text = "";
            this.burn_time.Value = 0;
        }

        private void copy_furn_Click(object sender, EventArgs e)
        {
            //Copy the furnace minetweaker output to the clipboard.
            Clipboard.SetText(mto_f.Text);
        }

        private void gen_less_Click(object sender, EventArgs e)
        {
            //Array of Text from boxes
            string[] cells = new string[] { X1.Text, Y1.Text, Z1.Text, X2.Text, Y2.Text, Z2.Text, X3.Text, Y3.Text, Z3.Text };
            List<string> final_cells = new List<string>();
            //Process array to determine which cells are full, adds full cells to list
            for (int i = 0; i < cells.Length; i++)
            {
                if(cells[i].Length == 0)
                {
                    //If cell is empty, continue to the next one
                    continue;
                }
                else
                {
                    // If cell is full add its contents to the final_cells list
                      final_cells.Add(cells[i]);
                }
            }
            //Join the list to one string
            string content = string.Join(">, <", final_cells);


            if (!string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(out_less.Text))
            {
                //If content is set, and the output is set, print minetweaker output
                this.mto_less.Text = "recipes.addShapeless(<" + out_less.Text + "> * " + amount_less.Value + ", [<" + content + ">])";
            }
            else if (string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(out_less.Text))
            {
                //If the content (crafting grid) is not filled out, but the output is filled out throw an error
                MessageBox.Show("Please use the crafting grid to make a recipe! (It is laid out like a crafting grid so it looks familiar, item locations will not matter for the output!)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!string.IsNullOrWhiteSpace(content) && string.IsNullOrWhiteSpace(out_less.Text))
            {
                // If the output is not set, but the recipe is made, throw an error
                MessageBox.Show("Please set an output for this recipe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //If nothing is set, throw an error
                MessageBox.Show("Please use the crafting grid to make a recipe! (It is laid out like a crafting grid so it looks familiar, item locations will not matter for the output!)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clear_less_Click(object sender, EventArgs e)
        {
            //Reset X
            X1.Text = null;
            X2.Text = null;
            X3.Text = null;

            //Reset Y
            Y1.Text = null;
            Y2.Text = null;
            Y3.Text = null;

            //Reset Z
            Z1.Text = null;
            Z2.Text = null;
            Z3.Text = null;

            //Reset output
            out_less.Text = null;
            amount_less.Value = 1;

            //Reset minetweaker output
            mto_less.Text = null;
        }

        private void copy_less_Click(object sender, EventArgs e)
        {
            //Set the clipboard text to the Minetweaker output
            Clipboard.SetText(mto_less.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ic2 = new ic2();

            ic2.Show();
        }
    }
}