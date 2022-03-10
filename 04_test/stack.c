#include <stdio.h>
#include <stdlib.h>

typedef struct stack_data_t
{
  int top;
  unsigned size;
  unsigned capacity;
} stack_data_t;

static int* StackCreate(stack_data_t* stackData, unsigned capacity)
{
  int* stack = 0;

  stackData->top = -1;
  stackData->size = 0;
  stackData->capacity = capacity;

  stack = (int*)malloc(stackData->capacity * sizeof(int));
  memset(stack, 0, sizeof(stack));

  return stack;
}

static void StackDestroy(stack_data_t* stackData, int* stack)
{
  stackData->top = -1;
  stackData->size = 0;
  stackData->capacity = 0;

  free(stack);
}

static int StackPush(stack_data_t* stackData, int* stack, int element)
{
  int result = 0;

  if (stackData->size == stackData->capacity)
  {
    result = 0;
  }
  else
  {
    ++stackData->top;
    stack[stackData->top] = element;
    ++stackData->size;
    result = 1;
  }

  return result;
}

static int StackPop(stack_data_t* stackData, int* stack)
{
  int result = 0;

  if (stackData->top == -1 || stackData->size == 0 || stackData->capacity == 0)
  {
    result = 0;
  }
  else
  {
    result = stack[stackData->top];
    --stackData->top;
    --stackData->size;
  }

  return result;
}

static int StackTop(stack_data_t* stackData, int* stack)
{
  return stack[stackData->top];
}

static int StackIsEmpty(stack_data_t* stackData)
{
  return stackData->size == 0;
}

static void StackPrint(stack_data_t* stackData, int* stack)
{
  int i;
  for (i = 0; i < stackData->size; ++i)
  {
    printf("%d ", stack[i]);
  }
  printf("\n");
}

int main()
{
  stack_data_t stackData;
  int* stack = StackCreate(&stackData, 10);

  StackPush(&stackData, stack, 11);
  StackPush(&stackData, stack, 22);
  StackPush(&stackData, stack, 33);
  StackPush(&stackData, stack, 44);
  StackPush(&stackData, stack, 55);
  StackPush(&stackData, stack, 66);

  StackPrint(&stackData, stack);

  int popped = 0;
  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);

  StackPrint(&stackData, stack);

  printf("Stack is empty: %s\n", StackIsEmpty(&stackData) ? "YES" : "NO");
  printf("Stack top is: %d\n", StackTop(&stackData, stack));

  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(&stackData, stack);
  printf("Popped: %d\n", popped);

  printf("Stack is empty: %s\n", StackIsEmpty(&stackData) ? "YES" : "NO");

  StackDestroy(&stackData, stack);
  return 0;
}