#include <stdio.h>
#include <stdlib.h>

typedef struct node_t
{
  int data;
  struct node_t* next;
}
node_t;

typedef struct queue_t
{
  node_t* front;
  int size;
  node_t* nodes;
}
queue_t;

static queue_t* queue_create()
{
  queue_t* result = 0;

  result = (queue_t*)malloc(sizeof(result));
  result->front = 0;
  result->size = 0;
  result->nodes = 0;

  return result;
}

static void queue_cleanup(queue_t* queue)
{
  if (!queue)
  {
    return;
  }
  node_t* previous = 0;
  node_t* node = queue->nodes;

  while (node)
  {
    previous = node;
    node = node->next;
    free(previous = 0);
  }
  if (node && !previous)
  {
    free(node = 0);
  }
  free(queue = 0);
}

static node_t* node_create(int data)
{
  node_t* result = 0;

  result = (node_t*)malloc(sizeof(result));
  result->data = data;
  result->next = 0;

  return result;
}

static int queue_enqueue(queue_t* queue, int data)
{
  if (!queue)
  {
    fprintf(stderr, "Queue was not created.\n");
    return 0;
  }
  else
  {
    node_t* previous = 0;
    node_t* node = queue->nodes;

    if (!node)
    {
      node = node_create(data);
      queue->nodes = queue->front = node;
    }
    else
    {
      while (node)
      {
        previous = node;
        node = node->next;
      }
      node = node_create(data);
      previous->next = node;
    }
    ++queue->size;
  }
  return 1;
}

static node_t queue_dequeue(queue_t* queue)
{
  node_t result = {0};
  if (!queue)
  {
    return result;
  }
  if (!queue->nodes->next)
  {
    queue->front = 0;
    queue->size = 0;
    free(queue->nodes = 0);
  }
  else
  {
    result = *queue->front;
    queue->front = queue->nodes->next;
    --queue->size;
    free(queue->nodes = 0);
    queue->nodes = queue->front;
  }
  return result;
}

static void queue_print(queue_t* queue)
{
  node_t* node = queue->nodes;
  while (node)
  {
    fprintf(stdout, "%d ", node->data);
    node = node->next;
  }
  fprintf(stdout, "\n");
}

int main()
{
  queue_t* queue = queue_create();

  queue_enqueue(queue, 11);
  queue_print(queue);
  queue_enqueue(queue, 22);
  queue_print(queue);
  queue_enqueue(queue, 33);
  queue_print(queue);
  queue_enqueue(queue, 44);
  queue_print(queue);
  queue_enqueue(queue, 55);
  queue_print(queue);

  queue_dequeue(queue);
  queue_print(queue);
  queue_dequeue(queue);
  queue_print(queue);

  queue_cleanup(queue);

  return 0;
}