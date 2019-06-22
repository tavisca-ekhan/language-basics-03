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
      var cal = new int[protein.Length];
      var items = new int[protein.Length];

      for (int i = 0; i < cal.Length; i++)
      {
        cal[i] = (protein[i] + carbs[i]) * 5 + fat[i] * 9;
        items[i] = i;
      }

      for (var i = 0; i < result.Length; i++)
      {
        if (dietPlans[i] == "")
          result[i] = 0;
        else
        {
          var diet = dietPlans[i].ToCharArray();
          var item = getItemNumber(diet[0], protein, carbs, fat, cal, items).ToArray();
          if (diet.Length == 1)
            result[i] = item[0];
          else
          {
            for (var j = 0; j < diet.Length; j++)
            {
              item = getItemNumber(diet[j], protein, carbs, fat, cal, item).ToArray();
              if (item.Length == 1 || j + 1 == diet.Length)
              {
                result[i] = item[0];
                break;
              }
            }
          }
        }
      }
      return result;
    }

    public static List<int> getItemNumber(char dietChar, int[] protein, int[] carbs, int[] fat, int[] cal, int[] items)
    {
      var item = new List<int>();
      var itemNumber = new List<int>();
      switch (dietChar)
      {
        case 'P':
          for (var i = 0; i < items.Length; i++)
            item.Add(protein[items[i]]);
          for (var i = 0; i < protein.Length; i++)
            if (protein[i] == item.Max())
              itemNumber.Add(i);
          return itemNumber;

        case 'p':
          for (var i = 0; i < items.Length; i++)
            item.Add(protein[items[i]]);
          for (var i = 0; i < protein.Length; i++)
            if (protein[i] == item.Min())
              itemNumber.Add(i);
          return itemNumber;

        case 'C':
          for (var i = 0; i < items.Length; i++)
            item.Add(carbs[items[i]]);
          for (var i = 0; i < carbs.Length; i++)
            if (carbs[i] == item.Max())
              itemNumber.Add(i);
          return itemNumber;

        case 'c':
          for (var i = 0; i < items.Length; i++)
            item.Add(carbs[items[i]]);
          for (var i = 0; i < carbs.Length; i++)
            if (carbs[i] == item.Min())
              itemNumber.Add(i);
          return itemNumber;

        case 'F':
          for (var i = 0; i < items.Length; i++)
            item.Add(fat[items[i]]);
          for (var i = 0; i < fat.Length; i++)
            if (fat[i] == item.Max())
              itemNumber.Add(i);
          return itemNumber;

        case 'f':
          for (var i = 0; i < items.Length; i++)
            item.Add(fat[items[i]]);
          for (var i = 0; i < fat.Length; i++)
            if (fat[i] == item.Min())
              itemNumber.Add(i);
          return itemNumber;

        case 'T':
          for (var i = 0; i < items.Length; i++)
            item.Add(cal[items[i]]);
          for (var i = 0; i < cal.Length; i++)
            if (cal[i] == item.Max())
              itemNumber.Add(i);
          return itemNumber;

        case 't':
          for (var i = 0; i < items.Length; i++)
            item.Add(cal[items[i]]);
          for (var i = 0; i < cal.Length; i++)
            if (cal[i] == item.Min())
              itemNumber.Add(i);
          return itemNumber;

        default:
          return null;
      }
    }
  }
}
