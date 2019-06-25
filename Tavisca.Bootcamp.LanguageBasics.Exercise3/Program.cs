using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
  public static class Program
  {
    static void Main(string[] args)
    {
      Test(
          new[] { 3, 4 },
          new[] { 2, 8 },
          new[] { 5, 2 },
          new[] { "P", "p", "C", "c", "F", "f", "T", "t" },
          new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
      Test(
          new[] { 3, 4, 1, 5 },
          new[] { 2, 8, 5, 1 },
          new[] { 5, 2, 4, 4 },
          new[] { "tFc", "tF", "Ftc" },
          new[] { 3, 2, 0 });
      Test(
          new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 },
          new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 },
          new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 },
          new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" },
          new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
      Console.ReadKey(true);
    }

    private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
    {
      var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
      Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
      Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
      Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
      Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
      Console.WriteLine(result);
    }

    public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
    {
      var result = new int[dietPlans.Length];
      var calorie = new int[protein.Length];
      var items = new int[protein.Length];

      for (var calorieIndex = 0; calorieIndex < calorie.Length; calorieIndex++)
      {
        calorie[calorieIndex] = (protein[calorieIndex] + carbs[calorieIndex]) * 5 + fat[calorieIndex] * 9;
        items[calorieIndex] = calorieIndex;
      }

      for (var resultIndex = 0; resultIndex < result.Length; resultIndex++)
      {
        if (dietPlans[resultIndex] == "")
          result[resultIndex] = 0;
        else
        {
          var diet = dietPlans[resultIndex].ToCharArray();
          var item = GetItemNumber(diet[0], protein, carbs, fat, calorie, items).ToArray();
          if (diet.Length == 1)
            result[resultIndex] = item[0];
          else
          {
            for (var dietIndex = 0; dietIndex < diet.Length; dietIndex++)
            {
              item = GetItemNumber(diet[dietIndex], protein, carbs, fat, calorie, item).ToArray();
              if (item.Length == 1 || dietIndex + 1 == diet.Length)
              {
                result[resultIndex] = item[0];
                break;
              }
            }
          }
        }
      }
      return result;
    }

    public static List<int> GetItemNumber(char dietChar, int[] protein, int[] carbs, int[] fat, int[] calorie, int[] items)
    {
      var itemList = new List<int>();
      var itemNumber = new List<int>();
      switch (dietChar)
      {
        case 'P':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(protein[items[index]]);
          for (var index = 0; index < protein.Length; index++)
            if (protein[index] == itemList.Max())
              itemNumber.Add(index);
          return itemNumber;

        case 'p':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(protein[items[index]]);
          for (var index = 0; index < protein.Length; index++)
            if (protein[index] == itemList.Min())
              itemNumber.Add(index);
          return itemNumber;

        case 'C':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(carbs[items[index]]);
          for (var index = 0; index < carbs.Length; index++)
            if (carbs[index] == itemList.Max())
              itemNumber.Add(index);
          return itemNumber;

        case 'c':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(carbs[items[index]]);
          for (var index = 0; index < carbs.Length; index++)
            if (carbs[index] == itemList.Min())
              itemNumber.Add(index);
          return itemNumber;

        case 'F':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(fat[items[index]]);
          for (var index = 0; index < fat.Length; index++)
            if (fat[index] == itemList.Max())
              itemNumber.Add(index);
          return itemNumber;

        case 'f':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(fat[items[index]]);
          for (var index = 0; index < fat.Length; index++)
            if (fat[index] == itemList.Min())
              itemNumber.Add(index);
          return itemNumber;

        case 'T':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(calorie[items[index]]);
          for (var index = 0; index < calorie.Length; index++)
            if (calorie[index] == itemList.Max())
              itemNumber.Add(index);
          return itemNumber;

        case 't':
          for (var index = 0; index < items.Length; index++)
            itemList.Add(calorie[items[index]]);
          for (var index = 0; index < calorie.Length; index++)
            if (calorie[index] == itemList.Min())
              itemNumber.Add(index);
          return itemNumber;

        default:
          return null;
      }
    }
  }
}
