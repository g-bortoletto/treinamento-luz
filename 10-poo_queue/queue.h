#ifndef QUEUE_H
#define QUEUE_H

#include "node.h"


class Queue
{
private:
	Node* m_front;
	int   m_size;
	Node* m_nodes;

public:
	Queue();
	~Queue();
	void cleanup();

	void setFront(Node* front);
	void setSize(int size);
	void setNodes(Node* nodes);

	Node* getFront();
	int   getSize();
	Node* getNodes();

	void growSize();
	void shrinkSize();
	void enqueue(int data);
	Node* dequeue();
	void print();
};


#endif