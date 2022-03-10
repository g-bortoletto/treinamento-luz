#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include <windows.h>

static LARGE_INTEGER frequency;

static void Swap(int *x, int *y)
{
  int temp = *x;
  *x = *y;
  *y = temp;
}

static float BubbleSort(int *data, int size)
{

  LARGE_INTEGER start, end;
  QueryPerformanceCounter(&start);

  while (size > 1)
  {
    int newSize = 0;
    int i;
    for (i = 1; i < size; ++i)
    {
      if (data[i - 1] > data[i])
      {
        Swap(&data[i - 1], &data[i]);
        newSize = i;
      }
    }
    size = newSize;
  }

  QueryPerformanceCounter(&end);

  return (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
}

static float InsertionSort(int *data, int size)
{
  LARGE_INTEGER start, end;
  QueryPerformanceCounter(&start);

  int i, j;
  i = 1;
  while (i < size)
  {
    j = i;
    while (j > 0 && data[j - 1] > data[j])
    {
      Swap(&data[j - 1], &data[j]);
      --j;
    }
    ++i;
  }

  QueryPerformanceCounter(&end);

  return (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
}

static float SelectionSort(int *data, int size)
{
  LARGE_INTEGER start, end;
  QueryPerformanceCounter(&start);

  int i, j;
  for (i = 0; i < size - 1; ++i)
  {
    int min = i;
    for (j = i + 1; j < size; ++j)
    {
      if (data[min] > data[j])
      {
        min = j;
      }
    }

    if (min != i)
    {
      Swap(&data[min], &data[i]);
    }
  }

  QueryPerformanceCounter(&end);

  return (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
}

#define SIZE 10000
// #define PRINT_DATA

int main()
{
  srand(time(0));
  QueryPerformanceFrequency(&frequency);

  int data_0[SIZE], data_1[SIZE], data_2[SIZE], i;
  float sortTime;

  for (i = 0; i < SIZE; ++i)
  {
    data_0[i] = rand() % SIZE;
    data_1[i] = data_2[i] = data_0[i];
  #ifdef PRINT_DATA
    printf("%d ", data_0[i]);
  #endif
  }
  printf("\n\n");

  sortTime = BubbleSort(data_0, SIZE);
#ifdef PRINT_DATA
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_0[i]);
  }
  printf("\n");
#endif

  printf("- BUBBLE SORT ---- %f\n", sortTime);

  sortTime = InsertionSort(data_1, SIZE);
#ifdef PRINT_DATA
  printf("\n");
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_1[i]);
  }
  printf("\n");
#endif

  printf("- INSERTION SORT - %f\n", sortTime);

  sortTime = SelectionSort(data_2, SIZE);
#ifdef PRINT_DATA
  printf("\n");
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_2[i]);
  }
  printf("\n");
#endif

  printf("- SELECTION SORT - %f\n", sortTime);

  return 0;
}