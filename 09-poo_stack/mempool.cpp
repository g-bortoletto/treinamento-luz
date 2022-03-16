#include "mempool.h"

Mempool::Mempool()
{
	setLastIndex(-1);
	setNext(0);
	setData(new int[POOL_SIZE]);
}

Mempool::~Mempool()
{

}

void Mempool::setNext(Mempool* next)
{
	m_next = next;
}

void Mempool::setLastIndex(int lastIndex)
{
	m_lastIndex = lastIndex;
}

void Mempool::setData(int* data)
{
	m_data = data;
}

Mempool* Mempool::getNext()
{
	return m_next;
}

int Mempool::getLastIndex()
{
	return m_lastIndex;
}

int* Mempool::getData()
{
	return m_data;
}
