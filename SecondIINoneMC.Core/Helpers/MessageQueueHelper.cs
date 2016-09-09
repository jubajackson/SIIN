#region using directives
using System;
using System.Messaging;
using System.Runtime.Serialization.Formatters;
#endregion

namespace SecondIINoneMC.Core.Helpers
{
    public static class MessageQueueHelper
    {
        /// <summary>
        /// Returns the instance of the Queue
        /// </summary>
        /// <param name="queuePath">Name of the Queue for which we need to get the handle</param>
        /// <returns>Queue instance if valid, null if not. </returns>
        public static MessageQueue GetQueue(String queuePath)
        {
            MessageQueue returnQueue = null;

            //EventHelper.Debug("Queue Path", queuePath);

            try
            {
                if (!MessageQueue.Exists(queuePath))
                    returnQueue = MessageQueue.Create(queuePath);
                else
                    returnQueue = new MessageQueue(queuePath);
            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex);
                //throw;
                throw new ApplicationException("Unable to connect to source.", ex);
            }

            return returnQueue;
        }

        /// <summary>
        /// Sends a message to the queue
        /// </summary>
        /// <param name="queuePath">Name of the Queue to which the message needs to be sent</param>
        /// <param name="data">The data that needs to be sent</param>
        public static void Send(String queuePath, Object data)
        {
            System.Messaging.MessageQueue queue = null;
            System.Messaging.Message message = null;

            try
            {
                if (data != null)
                {
                    message = new System.Messaging.Message();

                    message.Formatter = new BinaryMessageFormatter();

                    queue = GetQueue(queuePath);

                    if (queue != null)
                    {
                        queue.Formatter = message.Formatter;

                        if (queue.Transactional)
                        {
                            SendTransactionalMessage(queue, data);
                        }
                        else
                        {
                            queue.Send(data);
                        }
                    }
                }
            }
            finally
            {
                if (queue != null)
                {
                    queue.Dispose();
                }
            }
        }

        /// <summary>
        /// Sends a message to the queue
        /// </summary>
        /// <param name="queue">Queue to which the message needs to be sent.</param>
        /// <param name="data">The data that needs to be sent</param>
        public static void Send(MessageQueue queue, Object data)
        {
            try
            {
                if (queue != null)
                {
                    queue.Formatter = new BinaryMessageFormatter(
                                        FormatterAssemblyStyle.Simple,
                                        FormatterTypeStyle.TypesAlways);

                    if (queue.Transactional)
                        SendTransactionalMessage(queue, data);
                    else
                        queue.Send(data);
                }
                else
                {
                    throw new ApplicationException("Message Queue object is null");
                }
            }
            finally
            {
                if (queue != null)
                {
                    queue.Dispose();
                }
            }
        }

        /// <summary>
        /// Receives a message from the Queue..
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public static Object Receive(MessageQueue queue)
        {
            Object messageBody = null;

            try
            {
                if (queue.Transactional)
                {
                    messageBody = ReceiveTransactionalMessage(queue);
                }
                else
                {
                    messageBody = ReceiveMessage(queue);
                }
            }
            finally
            {
                if (queue != null)
                {
                    queue.Dispose();
                }
            }
            return messageBody;

        }

        /// <summary>
        /// This method needs to be called from Asynch Peek Handler
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public static Object CompleteAsynchronousPeek(
            MessageQueue queue,
            PeekCompletedEventArgs asyncResult)
        {
            System.Messaging.Message message;
            Object messageBody = null;

            if (queue != null)
            {
                // Retrieve the current Message that was spooled by EndReceive (asynchronous call).
                message = queue.EndPeek(asyncResult.AsyncResult);

                message.Formatter = new BinaryMessageFormatter(
                                        FormatterAssemblyStyle.Simple,
                                        FormatterTypeStyle.TypesAlways);

                messageBody = message.Body;
            }

            return messageBody;
        }

        /// <summary>
        /// Receives a message from the queue.
        /// </summary>
        /// <param name="queue">Queue from which the message needs to be retrieved</param>
        /// <returns>Body of the message</returns>
        private static Object ReceiveMessage(MessageQueue queue)
        {
            Object messageBody = null;
            System.Messaging.Message message;

            message = queue.Receive(new TimeSpan(0, 0, 0, 1));

            if (message != null)
            {
                // Get the message body
                message.Formatter = new BinaryMessageFormatter(
                                        FormatterAssemblyStyle.Simple,
                                        FormatterTypeStyle.TypesAlways);
                messageBody = message.Body;
            }

            return messageBody;
        }

        /// <summary>
        /// Sends a message to the transactional queue..
        /// </summary>
        /// <param name="queue">Queue to which the message needs to be sent.</param>
        /// <param name="data">The data that needs to be sent</param>
        private static void SendTransactionalMessage(MessageQueue queue, Object data)
        {
            MessageQueueTransaction queueTransaction = null;

            try
            {
                queueTransaction = new MessageQueueTransaction();

                queueTransaction.Begin();
                queue.DefaultPropertiesToSend.Priority = MessagePriority.Normal;
                queue.Send(data, queueTransaction);
                queueTransaction.Commit();
            }
            catch (System.Exception)
            {
                if (
                        queueTransaction != null &&
                        queueTransaction.Status == MessageQueueTransactionStatus.Pending
                    )
                {
                    queueTransaction.Abort();
                }

                throw;
            }
            finally
            {
                if (queueTransaction != null)
                {
                    queueTransaction.Dispose();
                    queueTransaction = null;
                }
            }
        }

        /// <summary>
        /// Receives a transactional message from the queue.
        /// </summary>
        /// <param name="queue">Queue from which the message needs to be received</param>
        private static Object ReceiveTransactionalMessage(MessageQueue queue)
        {
            MessageQueueTransaction queueTransaction = null;
            Object messageBody = null;
            System.Messaging.Message message;

            try
            {
                queueTransaction = new MessageQueueTransaction();
                queueTransaction.Begin();
                message = queue.Receive(queueTransaction);

                if (message != null)
                {
                    // Get the message body
                    message.Formatter = new BinaryMessageFormatter(
                                            FormatterAssemblyStyle.Simple,
                                            FormatterTypeStyle.TypesAlways);

                    messageBody = message.Body;
                }

                queueTransaction.Commit();
            }
            catch (System.Exception)
            {
                if (
                        queueTransaction != null &&
                        (queueTransaction.Status == MessageQueueTransactionStatus.Pending)
                    )
                {
                    queueTransaction.Abort();
                }

                throw;
            }
            finally
            {
                if (queueTransaction != null)
                {
                    queueTransaction.Dispose();
                    queueTransaction = null;
                }
            }

            return messageBody;
        }
    }
}
