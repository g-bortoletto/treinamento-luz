#include <stdio.h>
#include <stdlib.h>

struct node_t
{
  int data;
  struct node_t* left;
  struct node_t* right;
};
typedef struct node_t node_t;

static node_t* node_create(int data)
{
  node_t* node = 0;

  node = (node_t*)malloc(sizeof(node));
  node->data = data;
  node->left = 0;
  node->right = 0;

  return node;
}

static int tree_search(node_t* root, int key)
{
  if (!root)
  {
    return 0;
  }

  if (key == root->data)
  {
    return 1;
  }

  if (key < root->data)
  {
    return tree_search(root->left, key);
  }
  else
  {
    return tree_search(root->right, key);
  }
}

static node_t* tree_insert(node_t* root, int data)
{
  node_t* result = 0;

  if (!root)
  {
    return node_create(data);
  }
  if (data < root->data)
  {
    root->left = tree_insert(root->left, data);
  }
  else
  {
    root->right = tree_insert(root->right, data);
  }

  result = root;

  return result;
}

static node_t* node_value_min(node_t* node)
{
  node_t* result = node;
  while (result && result->left)
  {
    result = result->left;
  }
  return result;
}

static node_t* tree_remove(node_t* root, int data)
{
  node_t* result = 0;

  if (!root)
  {
    return root;
  }

  if (data < root->data)
  {
    root->left = tree_remove(root->left, data);
  }
  else if (data > root->data)
  {
    root->right = tree_remove(root->right, data);
  }
  else
  {
    if (!root->left && !root->right)
    {
      return 0;
    }
    else if (!root->left)
    {
      node_t* aux = root->right;
      free(root);
      return aux;
    }
    else if (!root->right)
    {
      node_t* aux = root->left;
      free(root);
      return aux;
    }
    node_t* aux = node_value_min(root->right);
    root->data = aux->data;
    root->right = tree_remove(root->right, aux->data);
  }
  result = root;
  return result;
}

static node_t* tree_cleanup(node_t* root)
{
  if (!root)
  {
    return 0;
  }
  tree_cleanup(root->left);
  tree_cleanup(root->right);
  free(root = 0);

  return root;
}

static void tree_print_aux(node_t* root)
{
  if (!root)
  {
    return;
  }
  fprintf(stdout, "%d ", root->data);
  tree_print_aux(root->left);
  tree_print_aux(root->right);
}

static void tree_print(node_t* root)
{
  tree_print_aux(root);
  fprintf(stdout, "\n");
}

int main()
{
  node_t* root = 0;

  root = tree_insert(root, 33);
  tree_print(root);
  root = tree_insert(root, 55);
  tree_print(root);
  root = tree_insert(root, 11);
  tree_print(root);
  root = tree_insert(root, 22);
  tree_print(root);
  root = tree_insert(root, 44);
  tree_print(root);

  root = tree_remove(root, 55);
  tree_print(root);
  root = tree_remove(root, 11);
  tree_print(root);

  root = tree_cleanup(root);

  return 0;
}