#include <stdio.h>
#include <stdlib.h>

typedef struct link_t
{
  int data;
  struct link_t* next;
} link_t;

typedef struct linked_list_t
{
  link_t* first;
  link_t* last;
  int size;
} linked_list_t;

static linked_list_t* linked_list_create()
{
  linked_list_t* linked_list = 0;

  linked_list = (linked_list_t*)malloc(sizeof(linked_list));
  linked_list->first = 0;
  linked_list->last  = 0;
  linked_list->size  = 0;

  return linked_list;
}

static void linked_list_cleanup(linked_list_t* linked_list)
{
  if (linked_list->size > 0)
  {
    link_t* previous = 0;
    link_t* current  = linked_list->first;

    int links_freed = 0;

    while (current)
    {
      previous = current;
      if (!previous)
      {
        free(current = 0);
        ++links_freed;
      }
      else
      {
        current  = current->next;
        free(previous = 0);
        ++links_freed;
      }
    }
  }
  linked_list->first = 0;
  linked_list->last = 0;
  linked_list->size = 0;
  free(linked_list = 0);
}

static link_t* link_allocate()
{
  link_t* link = (link_t*)malloc(sizeof(link));
  memset(link, 0, sizeof(link));
  link->data = 0;
  link->next = 0;
  return link;
}

static int linked_list_add(linked_list_t* linked_list, int data)
{
  if (!linked_list)
  {
    return 0;
  }

  link_t* new_link = link_allocate();
  new_link->data = data;

  link_t* link = linked_list->first;
  link_t* previous = 0;

  while (link)
  {
    previous = link;
    link = link->next;
  }
  link = new_link;
  if (previous)
  {
    previous->next = link;
  }
  else
  {
    linked_list->first = link;
  }
  linked_list->last = link;
  ++linked_list->size;

  return 1;
}

static int linked_list_remove(linked_list_t* linked_list, int data)
{
  link_t* link = linked_list->first;
  link_t* previous = 0;

  while (link)
  {
    if (link->data == data)
    {
      if (previous)
      {
        previous->next = link->next;
        if (!previous->next)
        {
          linked_list->last = previous;
        }
      }
      else
      {
        linked_list->first = link->next;
      }
      free(link = 0);
      --linked_list->size;
      return 1;
    }
    previous = link;
    link = link->next;
  }

  return 0;
}

static link_t* linked_list_search(linked_list_t* linked_list, int key)
{
  link_t* found = 0;
  int i;

  found = linked_list->first;
  for (i = 0; i < linked_list->size; ++i)
  {
    if (found->data == key)
    {
      return found;
    }
    found = found->next;
  }
  found = 0;

  return found;
}

static void linked_list_print(linked_list_t* linked_list)
{
  link_t* link = linked_list->first;
  if (!link)
  {
    return;
  }
  do
  {
    fprintf(stdout, "%d ", link->data);
    link = link->next;
  }
  while (link);
  fprintf(stdout, "\n");
}

int main()
{
  linked_list_t* linked_list = 0;

  linked_list = linked_list_create();


  linked_list_add(linked_list, 11);
  linked_list_print(linked_list);
  linked_list_add(linked_list, 22);
  linked_list_print(linked_list);
  linked_list_add(linked_list, 33);
  linked_list_print(linked_list);
  linked_list_add(linked_list, 44);
  linked_list_print(linked_list);
  linked_list_add(linked_list, 55);
  linked_list_print(linked_list);
  linked_list_add(linked_list, 66);
  linked_list_print(linked_list);

  linked_list_remove(linked_list, 33);
  linked_list_print(linked_list);

  link_t* found = linked_list_search(linked_list, 33);

  linked_list_cleanup(linked_list);

  return 0;
}