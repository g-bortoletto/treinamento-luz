#include <stdio.h>
#include <stdlib.h>

typedef struct stack_t
{
  int top;
  unsigned size;
  unsigned capacity;
  int* data;
} stack_t;

static stack_t* StackCreate(unsigned capacity)
{
  stack_t* stack = 0;
  stack = (stack_t*)malloc(3 * sizeof(int) + sizeof(int*));

  stack->top = -1;
  stack->size = 0;
  stack->capacity = capacity;
  stack->data = (int*)malloc(stack->capacity * sizeof(int));
  memset(stack->data, 0, sizeof(stack->data));

  return stack;
}

static void StackDestroy(stack_t* stack)
{
  stack->top = -1;
  stack->size = 0;
  stack->capacity = 0;

  free(stack->data = 0);
  free(stack = 0);
}

static int StackPush(stack_t* stack, int element)
{
  int result = 0;

  if (stack->size == stack->capacity)
  {
    result = 0;
  }
  else
  {
    ++stack->top;
    stack->data[stack->top] = element;
    ++stack->size;
    result = 1;
  }

  return result;
}

static int StackPop(stack_t* stack)
{
  int result = 0;

  if (stack->top == -1 || stack->size == 0 || stack->capacity == 0)
  {
    result = 0;
  }
  else
  {
    result = stack->data[stack->top];
    stack->data[stack->top] = 0;
    --stack->top;
    --stack->size;
  }

  return result;
}

static int StackTop(stack_t* stack)
{
  return stack->data[stack->top];
}

static int StackIsEmpty(stack_t* stack)
{
  return stack->size == 0;
}

static void StackPrint(stack_t* stack)
{
  int i;
  for (i = 0; i < stack->size; ++i)
  {
    printf("%d ", stack->data[i]);
  }
  printf("\n");
}

int main()
{
  stack_t* stack;
  stack = StackCreate(10);

  StackPush(stack, 11);
  StackPush(stack, 22);
  StackPush(stack, 33);
  StackPush(stack, 44);
  StackPush(stack, 55);
  StackPush(stack, 66);

  StackPrint(stack);

  int popped = 0;
  popped = StackPop(stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(stack);
  printf("Popped: %d\n", popped);

  StackPrint(stack);

  printf("Stack is empty: %s\n", StackIsEmpty(stack) ? "YES" : "NO");
  printf("Stack top is: %d\n", StackTop(stack));

  popped = StackPop(stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(stack);
  printf("Popped: %d\n", popped);
  popped = StackPop(stack);
  printf("Popped: %d\n", popped);

  printf("Stack is empty: %s\n", StackIsEmpty(stack) ? "YES" : "NO");

  StackDestroy(stack);
  return 0;
}