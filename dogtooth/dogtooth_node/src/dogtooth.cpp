#include "dogtooth_node/dogtooth.hpp"

#include <memory>
#include <string>

using robotis::dogtooth::Dogtooth;
using namespace std::chrono_literals;

Dogtooth::Dogtooth(const std::string & port)
    : Node("dogtooth_node", rclcpp::NodeOptions().use_intra_process_comms(true))
{
  RCLCPP_INFO(get_logger(), "Init Dogtooth Node Main");
  node_handle_ = std::shared_ptr<::rclcpp::Node>(this, [](::rclcpp::Node *) {});
  check_device_status();
  run();
}

Dogtooth::Wheels * Dogtooth::get_wheels()
{
  return &wheels_;
}

Dogtooth::Motors * Dogtooth::get_motors()
{
  return &motors_;
}

void Dogtooth::check_device_status()
{
  
}

void Dogtooth::run()
{
  RCLCPP_INFO(this->get_logger(), "Run!");
  publish_timer(std::chrono::milliseconds(50));
  heartbeat_timer(std::chrono::milliseconds(100));
  parameter_event_callback();
  cmd_vel_callback();
}

void Dogtooth::publish_timer(const std::chrono::milliseconds timeout)
{
  publish_timer_ = this->create_wall_timer(
    timeout,
    [this]() -> void
    {
      rclcpp::Time now = this->now();

    }
  );
}

void Dogtooth::heartbeat_timer(const std::chrono::milliseconds timeout)
{
  heartbeat_timer_ = this->create_wall_timer(
    timeout,
    [this]() -> void
    {
      static uint8_t count = 0;
      std::string msg;
      //TODO  
      RCLCPP_DEBUG(this->get_logger(), "hearbeat count : %d, msg : %s", count, msg.c_str());
      count++;
    }
  );
}

void Dogtooth::parameter_event_callback()
{
  priv_parameters_client_ = std::make_shared<rclcpp::AsyncParametersClient>(this);
  while (!priv_parameters_client_->wait_for_service(std::chrono::seconds(1))) {
    if (!rclcpp::ok()) {
      RCLCPP_ERROR(this->get_logger(), "Interrupted while waiting for the service. Exiting.");
      return;
    }
    RCLCPP_WARN(this->get_logger(), "service not available, waiting again...");
  }

  auto param_event_callback =
    [this](const rcl_interfaces::msg::ParameterEvent::SharedPtr event) -> void
    {
      
    };
  parameter_event_sub_ = priv_parameters_client_->on_parameter_event(param_event_callback);
}

void Dogtooth::cmd_vel_callback()
{
  auto qos = rclcpp::QoS(rclcpp::KeepLast(10));
  cmd_vel_sub_ = this->create_subscription<geometry_msgs::msg::Twist>(
    "cmd_vel",
    qos,
    [this](const geometry_msgs::msg::Twist::SharedPtr msg) -> void
    {
      std::string sdk_msg;
      RCLCPP_DEBUG(
        this->get_logger(),
        "lin_vel: %f ang_vel: %f msg : %s", msg->linear.x, msg->angular.z, sdk_msg.c_str());
    }
  );
}
