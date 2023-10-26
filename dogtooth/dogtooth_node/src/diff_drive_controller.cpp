#include "dogtooth_node/diff_drive_controller.hpp"

#include <memory>

using robotis::dogtooth::DiffDriveController;

DiffDriveController::DiffDriveController(const float wheel_seperation, const float wheel_radius)
: Node("diff_drive_controller", rclcpp::NodeOptions().use_intra_process_comms(true))
{
  nh_ = std::shared_ptr<::rclcpp::Node>(this, [](::rclcpp::Node *) {});

  odometry_ = std::make_unique<Odometry>(
    nh_,
    wheel_seperation,
    wheel_radius);

  RCLCPP_INFO(this->get_logger(), "Run!");
}
