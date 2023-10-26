#include "dogtooth_node/devices/sound.hpp"

#include <memory>
#include <string>

using robotis::dogtooth::devices::Sound;

Sound::Sound(std::shared_ptr<rclcpp::Node> & nh, const std::string & server_name): Devices(nh)
{
  RCLCPP_INFO(nh_->get_logger(), "Succeeded to create sound server");
  srv_ = nh_->create_service<dogtooth_msgs::srv::Sound>(
    server_name,
    [this](
      const std::shared_ptr<dogtooth_msgs::srv::Sound::Request> request,
      std::shared_ptr<dogtooth_msgs::srv::Sound::Response> response) -> void
    {
      this->command(static_cast<void *>(request.get()), static_cast<void *>(response.get()));
    }
  );
}

void Sound::command(const void * request, void * response)
{
  dogtooth_msgs::srv::Sound::Request req = *(dogtooth_msgs::srv::Sound::Request *)request;
  dogtooth_msgs::srv::Sound::Response * res = (dogtooth_msgs::srv::Sound::Response *)response;
  // res->success = TODO 
}

void Sound::request(rclcpp::Client<dogtooth_msgs::srv::Sound>::SharedPtr client, dogtooth_msgs::srv::Sound::Request req)
{
  auto request = std::make_shared<dogtooth_msgs::srv::Sound::Request>(req);
  auto result = client->async_send_request(request);
}
