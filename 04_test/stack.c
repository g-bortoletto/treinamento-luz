#include <stdio.h>
#include <stdlib.h>

#define POOL_SIZE 4096

typedef struct mempool_t
{
  struct mempool_t* next;
  int lastIndex;
  int* data;
} mempool_t;

typedef struct stack_t
{
  int* top;
  unsigned size;
  mempool_t* data;
} stack_t;

static stack_t* stack_create()
{
  stack_t* result = 0;

  result = (stack_t*)malloc(sizeof(result));

  result->top = 0;
  result->size = 0;
  result->data = 0;

  return result;
}

static void stack_cleanup(stack_t* stack)
{
  mempool_t* previous = 0;
  mempool_t* pool = stack->data;

  while(pool->next)
  {
    previous = pool;
    pool = pool->next;
    free(previous->data = 0);
    free(previous = 0);
  }
  if (pool)
  {
    free(pool->data = 0);
    free(pool = 0);
  }

  free(stack = 0);
}

static mempool_t* pool_allocate()
{
  mempool_t* result = 0;

  result = (mempool_t*)malloc(sizeof(mempool_t*) + sizeof(int) + sizeof(int*));
  result->lastIndex = -1;
  result->next = 0;
  result->data = (int*)malloc(POOL_SIZE * sizeof(int));
  memset(result, 0, sizeof(result));

  return result;
}

static int stack_push(stack_t* stack, int data)
{
  int result = 0;
  mempool_t* pool = stack->data;
  if (!pool)
  {
    pool = pool_allocate();
    stack->data = pool;
  }
  else
  {
    while (pool->next)
    {
      pool = pool->next;
    }
    if (pool->lastIndex >= POOL_SIZE - 1)
    {
      mempool_t* new_pool = pool_allocate();
      pool->next = new_pool;
      pool = pool->next;
    }
  }
  ++pool->lastIndex;
  pool->data[pool->lastIndex] = data;
  stack->top = &pool->data[pool->lastIndex];
  ++stack->size;
  result = 1;
  return result;
}

static int stack_pop(stack_t* stack)
{
  if (!stack->size)
  {
    return 0;
  }
  mempool_t* previous = 0;
  mempool_t* pool = stack->data;
  while (pool->next)
  {
    previous = pool;
    pool = pool->next;
  }
  int result = pool->data[pool->lastIndex];
  pool->data[pool->lastIndex] = 0;
  --pool->lastIndex;
  if (pool->lastIndex < 0 && previous)
  {
    previous->next = 0;
    free(pool);
  }
  if (previous)
  {
    stack->top = &previous->data[previous->lastIndex];
  }
  else
  {
    stack->top = &pool->data[pool->lastIndex];
  }
  --stack->size;

  return result;
}

static int stack_peek(stack_t* stack)
{
  return *stack->top;
}

static void stack_print(stack_t* stack)
{
  if (!stack || !stack->data)
  {
    return;
  }

  mempool_t* pool = stack->data;
  int i = 0;

  do
  {
    for (i = 0; i <= pool->lastIndex; ++i)
    {
      fprintf(stdout, "%d ", pool->data[i]);
    }
    pool = pool->next;
  }
  while (pool);
  fprintf(stdout, "\n");
}

int main()
{
  stack_t* stack = stack_create();

  if (stack_push(stack, 11))
  {
    stack_print(stack);
  }
  if (stack_push(stack, 22))
  {
    stack_print(stack);
  };
  if (stack_push(stack, 33))
  {
    stack_print(stack);
  };
  if (stack_push(stack, 44))
  {
    stack_print(stack);
  }

  int popped = stack_pop(stack);
  stack_print(stack);
  fprintf(stdout, "popped %d\n", popped);
  popped = stack_pop(stack);
  stack_print(stack);
  fprintf(stdout, "popped %d\n", popped);
  popped = stack_pop(stack);
  stack_print(stack);
  fprintf(stdout, "popped %d\n", popped);
  popped = stack_pop(stack);
  stack_print(stack);
  fprintf(stdout, "popped %d\n", popped);

  stack_push(stack, 11);

  if (stack_push(stack, 11))
  {
    stack_print(stack);
  }
  if (stack_push(stack, 22))
  {
    stack_print(stack);
  };
  if (stack_push(stack, 33))
  {
    stack_print(stack);
  };
  if (stack_push(stack, 44))
  {
    stack_print(stack);
  }

  stack_cleanup(stack);

  return 0;
}