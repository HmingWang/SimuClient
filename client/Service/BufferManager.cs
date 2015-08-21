using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncService
{
    // This class creates a single large buffer which can be divided up 
    // and assigned to SocketAsyncEventArgs objects for use with each 
    // socket I/O operation.  
    // This enables bufffers to be easily reused and guards against 
    // fragmenting heap memory.
    // 
    // The operations exposed on the BufferManager class are not thread safe.
    class BufferManager
    {
        //缓冲区总大小
        int m_numBytes;                 // the total number of bytes controlled by the buffer pool
        //缓冲区
        byte[] m_buffer;                // the underlying byte array maintained by the Buffer Manager
        //空闲索引栈
        Stack<int> m_freeIndexPool;     // 
        //当前索引位置
        int m_currentIndex;
        //单位缓冲区大小
        int m_bufferSize;

        public BufferManager(int totalBytes, int bufferSize)
        {
            m_numBytes = totalBytes;//总缓冲大小
            m_currentIndex = 0;//缓冲区索引
            m_bufferSize = bufferSize;//缓冲大小
            m_freeIndexPool = new Stack<int>();
        }

        // Allocates buffer space used by the buffer pool
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            m_buffer = new byte[m_numBytes];//申请缓冲空间
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        //
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (m_freeIndexPool.Count > 0)//有空闲缓冲区
            {
                //参数：缓冲区首地址，偏移量，缓冲大小
                args.SetBuffer(m_buffer, m_freeIndexPool.Pop(), m_bufferSize);
            }
            else//没有空闲缓冲区
            {
                if ((m_numBytes - m_bufferSize) < m_currentIndex)
                {
                    //剩余缓冲区空间不足
                    return false;
                }
                //分配缓冲区，修改当前索引位置
                args.SetBuffer(m_buffer, m_currentIndex, m_bufferSize);
                m_currentIndex += m_bufferSize;
            }
            return true;
        }

        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)//释放缓冲区
        {
            //将缓冲加入空闲缓冲区索引, 清空事件缓冲
            m_freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

    }
}
