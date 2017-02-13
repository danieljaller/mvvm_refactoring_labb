﻿using System;
using PointManager.Models;
using System.Windows.Input;
using PointManager.Commands;
namespace PointManager.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel instans skapad: "+DateTime.Now);
            LoadModelCameraPosition();
            InitializeCommands();
        }

        private CameraPosition _modelCameraPosition;
        public CameraPosition ModelCameraPosition
        {
            get { return _modelCameraPosition; }
            set
            {
                _modelCameraPosition = value;
                OnPropertyChanged();
            }
        }

        private void LoadModelCameraPosition()
        {
            ModelCameraPosition = new CameraPosition()
            {
                Id = 1,
                PositionName = "Origo",
                cameraX = 0.35,
                cameraY = 0,
                cameraZ = 0,
                cameraDegH = 0,
                cameraDegV = 0
            };
        }

        private ICommand _saveCameraPositionCommand;
        public ICommand SaveCameraPositionCommand
        {
            get { return _saveCameraPositionCommand; }
            set { _saveCameraPositionCommand = value; OnPropertyChanged(); }
        }


        private void UpdateCameraPositionFunction()
        {
            ModelCameraPosition.PositionName += " * ";
        }

        private void InitializeCommands()
        {
            SaveCameraPositionCommand = new SaveCameraPositionCommand(UpdateCameraPositionFunction);
        }


    }
}