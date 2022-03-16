#ifndef MEMPOOL_H
#define MEMPOOL_H

#define POOL_SIZE 4096

class Mempool
{
private:
	Mempool* m_next;
	int      m_lastIndex;
	int*     m_data;

public:
	Mempool();
	~Mempool();

	void setNext(Mempool* next);
	void setLastIndex(int lastIndex);
	void setData(int* data);

	Mempool* getNext();
	int getLastIndex();
	int* getData();
};


#endif