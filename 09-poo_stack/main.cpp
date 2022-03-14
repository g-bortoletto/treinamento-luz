#include "node.cpp"
#include "stack.cpp"

int main()
{
	Stack<int>* stack = new Stack<int>();

	delete stack;
	return 0;
}