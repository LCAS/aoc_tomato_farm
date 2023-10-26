#ifndef DOGTOOTH_NODE__DIFF_DRIVE_CONTROLLER_HPP_
#define DOGTOOTH_NODE__DIFF_DRIVE_CONTROLLER_HPP_

#include <memory>

#include <rclcpp/rclcpp.hpp>

#include "dogtooth_node/odometry.hpp"

namespace robotis
{
  namespace dogtooth
  {
    class DiffDriveController : public rclcpp::Node
    {
      public:
        explicit DiffDriveController(const float wheel_seperation, const float wheel_radius);
        virtual ~DiffDriveController() {}

      private:
        std::shared_ptr<rclcpp::Node> nh_;
        std::unique_ptr<Odometry> odometry_;
    };
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__DIFF_DRIVE_CONTROLLER_HPP_
