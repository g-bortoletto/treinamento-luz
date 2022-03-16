#include <stdio.h>
#include <stdlib.h>

#include "mempool.cpp"
#include "stack.cpp"

int main()
{
	Stack* stack = new Stack();

	stack->push(11);
	stack->print();
	stack->push(22);
	stack->print();
	stack->push(33);
	stack->print();
	stack->push(44);
	stack->print();
	stack->push(55);
	stack->print();
	stack->push(66);
	stack->print();

	int popped = stack->pop();
	stack->print();
	popped = stack->pop();
	stack->print();
	popped = stack->pop();
	stack->print();
	popped = stack->pop();
	stack->print();

  	stack->cleanup();

	delete stack;

  	return 0;
}