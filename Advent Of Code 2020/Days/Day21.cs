using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent_Of_Code_2020.Days
{
    class Day21 : DayBase
    {
        public Day21(int day)
            : base(day)
        {
            
        }

        public override string SolvePart1()
        {
            var foods = GetFoods();
            var potentialIngredients = GetPotentialIngredient(foods);
            var allCandidates = new HashSet<string>(potentialIngredients.SelectMany(kv => kv.Value));
            return (foods.Sum(food => food.ingredients.Count(ingr => !allCandidates.Contains(ingr)))).ToString();
        }
        public override string SolvePart2()
        {
            var foods = GetFoods();
            var potentialIngredients = GetPotentialIngredient(foods);
            while (potentialIngredients.Any(kv => kv.Value.Count() > 1))
            {
                var singles = new HashSet<string>(potentialIngredients.Where(kv => kv.Value.Count() == 1).Select(kv => kv.Value.Single()));
                foreach (var kv in potentialIngredients)
                    if (kv.Value.Count() > 1) kv.Value.ExceptWith(singles);
            }

            return (string.Join(",", potentialIngredients.OrderBy(kv => kv.Key).Select(kv => kv.Value.Single())));
        }

        private List<(HashSet<string> ingredients, List<string> allergens)> GetFoods()
        {
            var input = File.ReadAllLines(_inputPath).ToList();

            List<(HashSet<string> ingredients, List<string> allergens)> foods = new List<(HashSet<string>, List<string>)>();
            foreach (var food in input)
            {
                var instance = food.Split("(contains ", StringSplitOptions.RemoveEmptyEntries).ToList();
                var ingredients = instance[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var allergen = instance[1].Split(new string[] { " ", ",", ")" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                foods.Add((ingredients.ToHashSet(), allergen));
            }

            return foods;
        }

        private Dictionary<string, HashSet<string>> GetPotentialIngredient(List<(HashSet<string> ingredients, List<string> allergens)> foods)
        {
            var allergens = foods.SelectMany(food => food.allergens).Distinct().ToArray();
            return allergens.Select(a =>
                    (allergen: a,
                     candidates: foods.Where(f => f.allergens.Contains(a))
                             .Select(f => f.ingredients)
                             .Aggregate((a, b) =>
                             {
                                 return new HashSet<string>(a.Intersect(b));
                             })))
                    .ToDictionary(a => a.Item1, a => a.Item2);
        }
    }
}
