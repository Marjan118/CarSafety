using System;
using System.Collections.Generic;
using Alarms;
using DataFusionModule;
using MapSource;
using PathSource;
using VehiclePhysics;

namespace SafetyModule
{
    /// <summary>
    /// Main class of safety logic
    /// </summary>
    public class SafetyModule : IDisposable
    {
        public SafetyModule(IDataSource dataSource, IMapProvider mapProvider, IPathProvider pathProvider,
            VPVehicleController carController, VPStandardInput input)
        {
            DataSource = dataSource;
            _mapProvider = mapProvider;
            _pathProvider = pathProvider;
            CarController = carController;
            Input = input;
            dataSource.DetectedObstacles += (sender, obstacles) => OnDataFusionArrive(obstacles);
        }


        #region Properties

        #region Config

        //TODO Add PID

        public float SafetyDistance { get; set; }

        /// <summary>
        /// Braking Accelerate, to measure distance od Braking
        /// </summary>
        //TODO ustawiąc to z modułu statystyk
        public float BrakingAccelerate { get; set; }

        #endregion
        /// <summary>
        /// Actual calculated braking distance of Car
        /// </summary>
        public float BrakingDistance { get; private set; }

        #endregion

        /// <summary>
        /// Source of fusion data module
        /// </summary>
        public IDataSource DataSource { get; }

        #region Fields

        private readonly IMapProvider _mapProvider;

        private readonly IPathProvider _pathProvider;

        private readonly RosClient.General.RosClient _rosClient = new RosClient.General.RosClient();

        /// <summary>
        /// Class from unity To get information about Car
        /// </summary>
        //TODO add middleware
        public readonly VPVehicleController CarController;

        /// <summary>
        /// Class from unity te control the car
        /// </summary>
        //TODO add middleware
        private readonly VPStandardInput Input;

        #endregion

        #region Methods

        /// <summary>
        /// Methods to calculate new arrived data from data fusion odule
        /// </summary>
        /// <param name="obstaclesInRangeOfView"></param>
        private void OnDataFusionArrive(IEnumerable<IObstacle> obstaclesInRangeOfView)
        {
            //TODO przerobić to na event od nowych obiektów w polu widenia
            foreach (var obstacle in obstaclesInRangeOfView)
            {
                if (_mapProvider.ObstacleExist(obstacle))
                {
                    continue;
                }

                //TODO Powinnienem sprawdzać path providera, czy nie wykonał ściażki na preszkodzie z mapy
                OnUnidentifiedObjectOnMap(obstacle);

                if (!_pathProvider.OnPath(obstacle))
                {
                    continue;
                }

                OnObjectOnPathDetected(obstacle);

                //TODO Event do systemu Path Provider o nadanie nowej ściezki

                if (this.CheckSafetyDistanceOfObstacle(CarController, obstacle))
                {
                    Input.ReleaseBrake();
                }
                else
                {
                    //TODO dodać uwzględnieni biegu przód/tył
                    var alarm = new Alarm(string.Empty, AlarmType.Critical);
                    OnAlarm(alarm);
                    Input.StopACar(CarController);
                }
            }
        }

        /// <summary>
        /// Own calculating data module
        /// </summary>
        public void Update()
        {
            BrakingDistance = this.CalculateBrakingDistance(CarController.speed);
        }

        public void Dispose()
        {
            _rosClient.RosSocket.Close();
        }

        #endregion

        #region event

        public event EventHandler<IObstacle> ObjectOnPathDetected;

        public event EventHandler<IObstacle> UnidentifiedObjectOnMap;

        public event EventHandler<Alarm> Alarm;

        #endregion

        #region Invoker

        /// <exception cref="T:System.Exception">A delegate callback throws an exception.</exception>
        protected virtual void OnObjectOnPathDetected(IObstacle e)
        {
            //TODo alarm text
            var alarm = new Alarm(string.Empty, AlarmType.Warning);
            OnAlarm(alarm);
            ObjectOnPathDetected?.Invoke(this, e);
        }

        /// <exception cref="T:System.Exception">A delegate callback throws an exception.</exception>
        protected virtual void OnUnidentifiedObjectOnMap(IObstacle e)
        {
            //TODo alarm text
            var alarm = new Alarm(string.Empty, AlarmType.Info);
            OnAlarm(alarm);
            UnidentifiedObjectOnMap?.Invoke(this, e);
        }

        /// <exception cref="T:System.Exception">A delegate callback throws an exception.</exception>
        protected virtual void OnAlarm(Alarm e)
        {
            Alarm?.Invoke(this, e);
        }

        #endregion
    }
}