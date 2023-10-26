
#include "dogtooth_node/sensors/battery_state.hpp"

#include <memory>
#include <string>
#include <utility>

using robotis::dogtooth::sensors::BatteryState;

BatteryState::BatteryState(std::shared_ptr<rclcpp::Node> & nh, const std::string & topic_name): Sensors(nh)
{
  pub_ = nh->create_publisher<sensor_msgs::msg::BatteryState>(topic_name, this->qos_);

  RCLCPP_INFO(nh_->get_logger(), "Succeeded to create battery state publisher");
}

void BatteryState::publish(const rclcpp::Time & now)
{
  auto msg = std::make_unique<sensor_msgs::msg::BatteryState>();
  msg->header.stamp = now;
  msg->design_capacity = 1.8f;
  // msg->voltage = TODO 
  // msg->percentage = TODO 
  pub_->publish(std::move(msg));
}
