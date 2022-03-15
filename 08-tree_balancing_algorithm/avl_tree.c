#include "avl_tree.h"

#include <stdio.h>
#include <stdlib.h>

#define COMPACT

/**
 * @brief Returns the highest between two numbers.
 *
 * @param x First number to be compared.
 * @param y Secont number to be compared.
 * @return int Highest of the two numbers.
 */
int m_max(int x, int y) { return x > y ? x : y; }

/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///

/**
 * @brief Frees a whole tree structure memory.
 *
 * @param root Pointer to tree structure root node.
 */
void treeDestroy(NodePtr root) {
  if (!root) { return; }
    treeDestroy(root->left);
    treeDestroy(root->right);
    free(root);
    root = 0;
}

/**
 * @brief Returns the height of a tree.
 *
 * @param root Root node of the tree structure.
 * @return int Integer value of tree structure height.
 */
int treeHeight(NodePtr root) {
  if (!root) { return 0; }
  int height_left  = treeHeight(root->left);
  int height_right = treeHeight(root->right);
  if (height_left > height_right) { return height_left + 1; }
  else { return height_right + 1; }
}

/**
 * @brief Calculates AVL tree balance factor.
 *
 * @param root AVL tree root node.
 * @return int Balance factor integer value.
 */
int treeBalance(NodePtr root) {
  if (!root) { return 0; }
  return treeHeight(root->right) - treeHeight(root->left);
}

/**
 * SOURCE: https://stackoverflow.com/questions/801740/c-how-to-draw-a-binary-tree-to-the-console
 */
int treePrintUtil(NodePtr root, int is_left, int offset, int depth, char s[20][255])
{
  char b[20];
  int width = 5;
  if (!root) return 0;
  sprintf(b, "(%03d)", root->value);
  int left  = treePrintUtil(root->left,  1, offset,                depth + 1, s);
  int right = treePrintUtil(root->right, 0, offset + left + width, depth + 1, s);
#ifdef COMPACT
  for (int i = 0; i < width; i++)
    s[depth][offset + left + i] = b[i];
  if (depth && is_left) {
    for (int i = 0; i < width + right; i++)
      s[depth - 1][offset + left + width/2 + i] = '-';
      s[depth - 1][offset + left + width/2] = '.';
  } else if (depth && !is_left) {
      for (int i = 0; i < left + width; i++)
        s[depth - 1][offset - width/2 + i] = '-';
        s[depth - 1][offset + left + width/2] = '.';
  }
#else
  for (int i = 0; i < width; i++)
    s[2 * depth][offset + left + i] = b[i];
  if (depth && is_left) {
    for (int i = 0; i < width + right; i++)
      s[2 * depth - 1][offset + left + width/2 + i] = '-';
      s[2 * depth - 1][offset + left + width/2] = '+';
      s[2 * depth - 1][offset + left + width + right + width/2] = '+';
    } else if (depth && !is_left) {
        for (int i = 0; i < left + width; i++)
          s[2 * depth - 1][offset - width/2 + i] = '-';
        s[2 * depth - 1][offset + left + width/2] = '+';
        s[2 * depth - 1][offset - width/2 - 1] = '+';
    }
#endif
    return left + width + right;
}

/**
 * @brief Prints a 2D tree to the console.
 *
 * @param root Pointer to the root of the tree to be printed.
 * SOURCE: https://stackoverflow.com/questions/801740/c-how-to-draw-a-binary-tree-to-the-console
 */
void treePrint2D(NodePtr root) {
  char s[20][255];
  for (int i = 0; i < 20; i++) {
    sprintf(s[i], "%80s", " ");
  }
  treePrintUtil(root, 0, 0, 0, s);
  for (int i = 0; i < 20; i++) {
    printf("%s\n", s[i]);
  }
}

/**
 * @brief Rotates root to the clockwise.
 *
 * @param root Root of branch to be rotated.
 */
void treeRightRotate(NodePtr *root) {
  NodePtr temp = (*root)->left;
  NodePtr temp_2 = temp->right;
  temp->right = (*root);
  (*root)->left = temp_2;
  (*root) = temp;
}

/**
 * @brief Rotates root to the counter-clockwise.
 *
 * @param root Root of branch to be rotated.
 */
void treeLeftRotate(NodePtr *root) {
  NodePtr temp = (*root)->right;
  NodePtr temp_2 = temp->left;
  temp->left = (*root);
  (*root)->right = temp_2;
  (*root) = temp;
}

/**
 * @brief Rotates root to the counter-clockwise and then clockwise.
 *
 * @param root Root of branch to be rotated.
 */
void treeLeftRightRotate(NodePtr *root) {
  treeLeftRotate(&((*root)->left));
  treeRightRotate(&(*root));
}

/**
 * @brief Rotates root to the clockwise and then counter-clockwise.
 *
 * @param root Root of branch to be rotated.
 */
void treeRightLeftRotate(NodePtr *root) {
  treeRightRotate(&((*root)->right));
  treeLeftRotate(&(*root));
}

/**
 * @brief Prints an avl tree structure in pre order.
 *
 * @param root Pointer to root of tree.
 */
void treePrintPre(NodePtr root) {
  if (!root) { return; }
  printf("%d ", root->value);
  treePrintPre(root->left);
  treePrintPre(root->right);
}

/**
 * @brief Prints an avl tree structure in order.
 *
 * @param root Pointer to root of tree.
 */
void treePrint(NodePtr root) {
  if (!root) { return; }
  treePrint(root->left);
  printf("%d ", root->value);
  treePrint(root->right);
}

/**
 * @brief Prints an avl tree structure in post order.
 *
 * @param root Pointer to root of tree.
 */
void treePrintPost(NodePtr root) {
  if (!root) { return; }
  treePrintPost(root->left);
  treePrintPost(root->right);
  printf("%d ", root->value);
}

/**
 * @brief Finds the total number of nodes of a tree.
 *
 * @param root Pointer to the root of the tree.
 * @return int Number of nodes of tree.
 */
int treeSize(NodePtr root) {
  int count = 0;
  if (!root) { return 0; }
  return count += treeSize(root->left) + treeSize(root->right) + 1;
}

/**
 * @brief Checks if value is inserted in the tree.
 *
 * @param root Pointer to the root of the tree.
 * @return int 0 if false and not-0 otherwise.
 */
int treeNodeSearch(NodePtr root, int value) {
  int found = 0;
  if (!root) { return found = 0; }
  if (value == root->value) { return found = 1; }
  else if (value < root->value) {
    found = treeNodeSearch(root->left, value);
  }
  else {
    found = treeNodeSearch(root->right, value);
  }
  return found;
}

/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///
/// ------------------------------------------------------------- ///

/**
 * @brief Allocates memmory to a new node.
 *
 * @param value Integer value of node.
 * @return NodePtr Pointer to new node allocated data.
 */
NodePtr nodeCreate(int value) {
  NodePtr node_new = 0;
  node_new = malloc(sizeof *node_new);
  node_new->value = value;
  node_new->left = 0;
  node_new->right = 0;
  return node_new;
}

/**
 * @brief Adds a new node to an AVL tree.
 *
 * @param tree_root AVL tree root node.
 * @param value New node value.
 * @return int 1 if success, 0 otherwise.
 */
int nodeInsert(NodePtr *root, int value) {
  if (!(*root)) { (*root) = nodeCreate(value); return 1; }
  if (value == (*root)->value) { return 0; }
  if (value < (*root)->value) {
    nodeInsert(&((*root)->left), value);
    if (treeBalance((*root)) < -1) {
      if (value < ((*root)->left)->value) { treeRightRotate(&(*root)); }
      else { treeLeftRightRotate(&(*root)); }
    }
  }
  if (value > (*root)->value) {
    nodeInsert(&((*root)->right), value);
    if (treeBalance((*root)) > 1) {
      if (value > ((*root)->right)->value) { treeLeftRotate(&(*root)); }
      else { treeRightLeftRotate(&(*root)); }
    }
  }
  return 0;
}

/**
 * @brief Finds the node which has the minimum value in the current tree.
 *
 * @param node Node which the search starts.
 * @return NodePtr The node with the minimum value.
 */
NodePtr nodeMinValue(NodePtr node) {
  NodePtr current = node;
  while (current->left != NULL)
    current = current->left;
  return current;
}

/**
 * @brief Destroys a node in a AVL tree by value.
 *
 * @param root Reference to the root of an AVL tree structure.
 * @param value Value of the node to be removed.
 * @return int 1 if success, 0 otherwise.
 */
int nodeRemove(NodePtr *root, int value)
{
  if (!(*root)) { return 0; }
  if (value < (*root)->value) { nodeRemove(&((*root)->left), value); }
  else if(value > (*root)->value) { nodeRemove(&((*root)->right), value); }
  else {
    if((!((*root)->left)) || (!((*root)->right))) {
      NodePtr temp = (*root)->left ? (*root)->left : (*root)->right;
      if (!temp) {
        temp = (*root);
        (*root) = 0;
      } else {
        (**root) = (*temp);
      }
      free(temp = 0);
    } else {
      NodePtr temp = nodeMinValue((*root)->right);
      (*root)->value = temp->value;
      nodeRemove(&((*root)->right), temp->value);
    }
  }
  if (!root) { return 0; }
  int balance = treeBalance((*root));
  if (balance > 1 &&  treeBalance((*root)->left)  >= 0) { treeRightRotate(&(*root)); }
  if (balance > 1 &&  treeBalance((*root)->left)   < 0) { treeLeftRightRotate(&(*root)); }
  if (balance < -1 && treeBalance((*root)->right) <= 0) { treeLeftRotate(&(*root)); }
  if (balance < -1 && treeBalance((*root)->right)  > 0) { treeRightLeftRotate(&(*root)); }
  return 1;
}