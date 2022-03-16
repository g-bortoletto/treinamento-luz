#include "stack.h"
#include <stdio.h>

Stack::Stack()
{
	setTop(0);
	setSize(0);
	setData(nullptr);
}

Stack::~Stack()
{
	cleanup();
}

void Stack::setTop(int *top)
{
	m_top = top;
}

void Stack::setSize(unsigned size)
{
	m_size = size;
}

void Stack::setData(Mempool *data)
{
	m_data = data;
}

int *Stack::getTop()
{
	return m_top;
}

unsigned Stack::getSize()
{
	return m_size;
}

Mempool *Stack::getData()
{
	return m_data;
}

void Stack::cleanup()
{
	Mempool *previous = 0;
	Mempool *pool = this->getData();

	while (pool->getNext())
	{
		previous = pool;
		pool = pool->getNext();
		delete previous;
	}

	if (pool)
	{
		delete pool;
	}
}

void Stack::push(int data)
{
	Mempool *pool = getData();
	if (!pool)
	{
		pool = new Mempool();
		setData(pool);
	}
	else
	{
		while (pool->getNext())
		{
			pool->setNext(pool->getNext());
		}
		if (pool->getLastIndex() >= POOL_SIZE - 1)
		{
			Mempool *new_pool = new Mempool();
			pool->setNext(new_pool);
			pool->setNext(pool->getNext());
		}
	}
	pool->setLastIndex(pool->getLastIndex() + 1);
	pool->getData()[pool->getLastIndex()] = data;
	setTop(&pool->getData()[pool->getLastIndex()]);
	setSize(getSize() + 1);
}

int Stack::pop()
{
	if (!getSize())
	{
		return 0;
	}
	Mempool *previous = 0;
	Mempool *pool = getData();
	while (pool->getNext())
	{
		previous = pool;
		pool->setNext(pool->getNext());
	}
	int result = pool->getData()[pool->getLastIndex()];
	pool->getData()[pool->getLastIndex()];
	pool->setLastIndex(pool->getLastIndex() - 1);

	if (pool->getLastIndex() < 0 && previous)
	{
		previous->getNext()->setNext(0);
		delete (pool);
	}
	if (previous)
	{
		setTop(&previous->getData()[previous->getLastIndex()]);
	}
	else
	{
		setTop(&pool->getData()[pool->getLastIndex()]);
	}
	setSize(getSize() - 1);
	return result;
}

int Stack::peek()
{
	return *getTop();
}

void Stack::print()
{
	if (!this || !this->getData())
	{
		return;
	}

	for (int i = 0; i < getSize(); ++i)
	{
		fprintf(stdout, "%d ", getData()->getData()[i]);
	}
	fprintf(stdout, "\n");
}
