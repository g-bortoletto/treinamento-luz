#include "avl_tree.c"
#include <stdio.h>

void consoleClear() { for (int i = 0; i < 1; ++i) { printf("\n\n\n\n\n\n\n\n\n\n"); } }
void consoleSpace() { printf("\n\n\n"); }

int menu() {
  printf("1.  Criar árvore\n");
  printf("2.  Liberar árvore\n");
  printf("3.  Inserir elemento\n");
  printf("4.  Remover elemento\n");
  printf("5.  Consultar elemento\n");
  printf("6.  Imprimir percurso pré-ordem\n");
  printf("7.  Imprimir percurso em ordem\n");
  printf("8.  Imprimir percurso pós-ordem\n");
  printf("9.  Altura da árvore\n");
  printf("10. Total de elementos\n");
  printf("11. Sair\n");
  printf("OP: ");
  int input = 0;
  scanf("%d", &input);
  return input == 11 ? -1 : input;;
}

void run() {
  int value = 0;
  int input = 0;
  consoleSpace();
  NodePtr root = 0;
  do {
    input = menu();
    consoleClear();
    switch(input) {
      case 1:
      case 3: {
        printf("Insira o valor a ser inserido na arvore: ");
        scanf("%d", &value);
        nodeInsert(&root, value);
        consoleSpace();
      break;
      }
      case 2: {
        treeDestroy(root);
        printf("Arvore destruida com sucesso!\n");
        consoleSpace();
      } break;
      case 4: {
        printf("Insira o valor a ser removido da arvore: ");
        scanf("%d", &value);
        nodeRemove(&root, value);
        consoleSpace();
      } break;
      case 5: {
        printf("Insira o valor da consulta: ");
        scanf("%d", &value);
        int found = treeNodeSearch(root, value);
        if (!found) {
          printf("Valor não encontrado.\n");
        } else {
          printf("Valor encontrado!\n");
        }
        consoleSpace();
      } break;
      case 6: {
        treePrintPre(root);
        consoleSpace();
      } break;
      case 7: {
        treePrint(root);
        consoleSpace();
      } break;
      case 8: {
        treePrintPost(root);
        consoleSpace();
      } break;
      case 9: {
        value = treeHeight(root);
        printf("Altura atual da arvore: %d.\n", value);
        consoleSpace();
      } break;
      case 10: {
        value = treeSize(root);
        printf("Numero atual de elementos da arvore: %d.\n", value);
        consoleSpace();
      } break;
      case 12: { // print 2d tree
        treePrint2D(root);
      } break;
      case -1: {
        printf("Saindo do programa...\n");
      } break;
      default: {
        printf("Opcao invalida!\n");
        consoleSpace();
      } break;
    }
  } while (input != -1);
}

int main(int argc, char *argv[]) {
  run();
  return 0;
}