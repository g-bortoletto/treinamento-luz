#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include <windows.h>

#define SIZE 10
#define PRINT_DATA











static void Swap(int *x, int *y)
{
  int temp = *x;
  *x = *y;
  *y = temp;
}











static void Merge(int *data, int left, int mid, int right)
{
  int leftSubArraySize = mid - left + 1;
  int rightSubArraySize = right - mid;

  int *leftSubArray = (int *)malloc(leftSubArraySize * sizeof(int));
  memset(leftSubArray, 0, sizeof leftSubArray);

  int *rightSubArray = (int *)malloc(rightSubArraySize * sizeof(int));
  memset(rightSubArray, 0, sizeof rightSubArray);

  int i;
  for (i = 0; i < leftSubArraySize; ++i)
  {
    leftSubArray[i] = data[left + i];
  }
  for (i = 0; i < rightSubArraySize; ++i)
  {
    rightSubArray[i] = data[mid + 1 + i];
  }

  i = 0;
  int j = 0;
  int k = left;

  while (i < leftSubArraySize && j < rightSubArraySize)
  {
    if (leftSubArray[i] < rightSubArray[j])
    {
      data[k] = leftSubArray[i];
      ++i;
    }
    else
    {
      data[k] = rightSubArray[j];
      ++j;
    }
    ++k;
  }

  while (i < leftSubArraySize)
  {
    data[k++] = leftSubArray[i++];
  }

  while (j < rightSubArraySize)
  {
    data[k++] = rightSubArray[j++];
  }

  free(rightSubArray);
  free(leftSubArray);
}

static void MergeSort(int *data, int left, int right)
{
  if (left < right)
  {
    int mid = (left + right) / 2;

    MergeSort(data, left, mid);
    MergeSort(data, mid + 1, right);

    Merge(data, left, mid, right);
  }
}











static void BubbleSort(int *data, int size)
{
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
}











static void InsertionSort(int *data, int size)
{
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
}











static void SelectionSort(int *data, int size)
{
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
}











int main()
{
  srand(time(0));
  LARGE_INTEGER frequency, start, end;
  QueryPerformanceFrequency(&frequency);


  int data_0[SIZE], data_1[SIZE], data_2[SIZE], data_3[SIZE], data_4[SIZE], i;
  float sortTime;

  for (i = 0; i < SIZE; ++i)
  {
    data_0[i] = rand() % SIZE;
    data_1[i] = data_4[i] = data_3[i] = data_2[i] = data_0[i];
#ifdef PRINT_DATA
    printf("%d ", data_0[i]);
#endif
  }
  printf("\n\n");

  QueryPerformanceCounter(&start);
  BubbleSort(data_0, SIZE);
  QueryPerformanceCounter(&end);
  sortTime = (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
#ifdef PRINT_DATA
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_0[i]);
  }
  printf("\n");
#endif

  printf("- BUBBLE SORT ---- %f\n", sortTime);

  QueryPerformanceCounter(&start);
  InsertionSort(data_1, SIZE);
  QueryPerformanceCounter(&end);
  sortTime = (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
#ifdef PRINT_DATA
  printf("\n");
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_1[i]);
  }
  printf("\n");
#endif

  printf("- INSERTION SORT - %f\n", sortTime);

  QueryPerformanceCounter(&start);
  SelectionSort(data_2, SIZE);
  QueryPerformanceCounter(&end);
  sortTime = (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
#ifdef PRINT_DATA
  printf("\n");
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_2[i]);
  }
  printf("\n");
#endif

  printf("- SELECTION SORT - %f\n", sortTime);

  QueryPerformanceCounter(&start);
  MergeSort(data_3, 0, SIZE - 1);
  QueryPerformanceCounter(&end);
  sortTime = (end.QuadPart - start.QuadPart) / (float)frequency.QuadPart;
#ifdef PRINT_DATA
  printf("\n");
  for (i = 0; i < SIZE; ++i)
  {
    printf("%d ", data_3[i]);
  }
  printf("\n");
#endif

  printf("- MERGE SORT ----- %f\n", sortTime);

  return 0;
}