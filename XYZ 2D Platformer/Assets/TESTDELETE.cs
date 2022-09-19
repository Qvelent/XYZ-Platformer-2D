using System.Linq;
using UnityEngine;

public class TESTDELETE : MonoBehaviour
{
    void Start()
    {
      Array();
    }

    private static void Array()
    {
        //===========================================================
        int[] numbers = { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
        int result = numbers.Count(number => number > 0);
        Debug.Log($"Число элементов больше нуля: {result}");
        
        
        //===========================================================
        int[] numbers1 = { -4, -3, -2, -1,0, 1, 2, 3, 4 };
             
        int n = numbers1.Length; // длина массива
        int k = n / 2;          // середина массива
        for(int i=0; i < k; i++)
        {
            var temp = numbers1[i]; // вспомогательный элемент для обмена значениями
            numbers1[i] = numbers[n - i - 1];
            numbers1[n - i - 1] = temp;
        }
        foreach(int i in numbers1)
        {
            Debug.Log($"{i} \t");
        }
        
        
        //===========================================================
        int[] nums = { 54, 7, -41, 2, 4, 2, 89, 33, -5, 12 };
 
        // сортировка
        for (int i = 0; i < nums.Length - 1; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] > nums[j])
                {
                    (nums[i], nums[j]) = (nums[j], nums[i]);
                }
            }
        }

        // вывод
        Debug.Log("Вывод отсортированного массива");
        foreach (var t in nums)
        {
            Debug.Log(t);
        }
    }
}
