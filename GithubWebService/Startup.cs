using GithubWebService.Controllers;
using GithubWebService.Models;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Messaging;
using System.Xml.Serialization;

[assembly: OwinStartupAttribute(typeof(GithubWebService.Startup))]
namespace GithubWebService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //messaing queue

            // Create an instance of MessageQueue. Set its formatter.
            MessageQueue myQueue = new MessageQueue(@".\private$\GithubQueue");
            myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });

            // Add an event handler for the ReceiveCompleted event.
            myQueue.ReceiveCompleted += new
                ReceiveCompletedEventHandler(MyReceiveCompleted);

            // Begin the asynchronous receive operation.
            myQueue.BeginReceive();

            // Do other work on the current thread.
        }

        private static void MyReceiveCompleted(Object source,
           ReceiveCompletedEventArgs asyncResult)
        {
            // Connect to the queue.
            MessageQueue mq = (MessageQueue)source;

            // End the asynchronous Receive operation.
            Message m = mq.EndReceive(asyncResult.AsyncResult);

            // Display message information on the screen.
            List<Repository> repositoryList = RepositoriesController.GetRepository((string)m.Body);

            XmlSerializer s = new XmlSerializer(typeof(List<Repository>));
            MemoryStream ms = new MemoryStream();
            s.Serialize(ms, repositoryList);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string serialized = sr.ReadToEnd();
            sr.Close();
            ms.Close();

            SendData(serialized);

            // Restart the asynchronous Receive operation.
            mq.BeginReceive();

            return;
        }

        public static void SendData(string msg)
        {
            const string sendQueueName = @".\private$\GithubQueueRepo";
            MessageQueue msMq = null;

            if (!MessageQueue.Exists(sendQueueName))
            {
                msMq = MessageQueue.Create(sendQueueName);
            }
            else
            {
                msMq = new MessageQueue(sendQueueName);

            }

            try
            {
                msMq.Send(msg);

            }

            catch (MessageQueueException ee)
            {
                Console.Write(ee.ToString());
            }

            catch (Exception eee)
            {
                Console.Write(eee.ToString());
            }

            finally
            {
                msMq.Close();
            }

        }
    }

}
