#ifndef STACK_H
#define STACK_H

#include "mempool.h"

class Stack
{
private:
	int* m_top;
	unsigned m_size;
	Mempool* m_data;

public:
	Stack();
	~Stack();

	void setTop(int* top);
	void setSize(unsigned size);
	void setData(Mempool* data);

	int* getTop();
	unsigned getSize();
	Mempool* getData();

	void cleanup();
	void push(int data);
	int pop();
	int peek();
	void print();
};


#endif