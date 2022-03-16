#include "node.cpp"
#include "queue.cpp"

int main()
{
	Queue *queue = new Queue();

	queue->enqueue(11);
	queue->print();
	queue->enqueue(22);
	queue->print();
	queue->enqueue(33);
	queue->print();
	queue->enqueue(44);
	queue->print();
	queue->enqueue(55);
	queue->print();

	queue->dequeue();
	queue->print();
	queue->dequeue();
	queue->print();

	queue->cleanup();

	delete queue;

	return 0;
}