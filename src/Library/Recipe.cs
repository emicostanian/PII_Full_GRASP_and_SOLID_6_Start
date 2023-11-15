//-------------------------------------------------------------------------
// <copyright file="Recipe.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Full_GRASP_And_SOLID
{
    public class Recipe : IRecipeContent
    {
        private IList<BaseStep> steps = new List<BaseStep>();

        public Product FinalProduct { get; set; }

        public bool Cooked { get; private set; } = false;

        public void AddStep(Product input, double quantity, Equipment equipment, int time)
        {
            Step step = new Step(input, quantity, equipment, time);
            this.steps.Add(step);
        }

        public void AddStep(string description, int time)
        {
            WaitStep step = new WaitStep(description, time);
            this.steps.Add(step);
        }

        public void RemoveStep(BaseStep step)
        {
            this.steps.Remove(step);
        }

        public string GetTextToPrint()
        {
            if (this.FinalProduct == null)
            {
                throw new InvalidOperationException("FinalProduct cannot be null.");
            }

            StringBuilder result = new StringBuilder($"Receta de {this.FinalProduct.Description}:\n");

            foreach (BaseStep step in this.steps)
            {
                result.AppendLine(step.GetTextToPrint());
            }

            result.AppendLine($"Costo de producción: {this.GetProductionCost()}");
            result.AppendLine($"Tiempo total de cocción: {this.GetCookTime()} minutos");
            result.AppendLine($"¿Cocido? {this.Cooked}");

            return result.ToString();
        }

        public double GetProductionCost()
        {
            double result = 0;

            foreach (BaseStep step in this.steps)
            {
                result += step.GetStepCost();
            }

            return result;
        }

        public int GetCookTime()
        {
            int totalCookTime = 0;

            foreach (BaseStep step in this.steps)
            {
                if (step is Step)
                {
                    totalCookTime += ((Step)step).Time; 
                }
            }

            return totalCookTime;
        }

        public void Cook()
        {
            int cookTime = GetCookTime();

            Cooked = true;
        }
    

    public class RecipeAdapter : TimerClient
    {
        private Recipe recipe;
        public RecipeAdapter(Recipe recipe)
        {
            this.recipe = recipe;
        }
        public void TimeOut()
        {
            this.recipe.Cooked=true;
        }
    }

}
}

// Que patrón(es) o principio(s) has usado para esto?
// SRP, DIP E ISP, GRASP y SOLID.