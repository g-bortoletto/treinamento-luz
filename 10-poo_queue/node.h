#ifndef NODE_H
#define NODE_H


class Node
{
private:
	int 	m_data;
	Node*	m_next;

public:
	Node(int data);

	void setData(int data);
	void setNext(Node* next);

	int getData();
	Node* getNext();
};


#endif