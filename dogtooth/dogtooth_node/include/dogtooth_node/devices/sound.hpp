#ifndef DOGTOOTH_NODE__DEVICES__SOUND_HPP_
#define DOGTOOTH_NODE__DEVICES__SOUND_HPP_

#include <dogtooth_msgs/srv/sound.hpp>
#include <memory>
#include <string>
#include "dogtooth_node/devices/devices.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace devices
    {
      class Sound : public Devices
      {
        public:
          static void request(
            rclcpp::Client<dogtooth_msgs::srv::Sound>::SharedPtr client,
            dogtooth_msgs::srv::Sound::Request req);

          explicit Sound(
            std::shared_ptr<rclcpp::Node> & nh,
            const std::string & server_name = "sound");

          void command(const void * request, void * response) override;

        private:
          rclcpp::Service<dogtooth_msgs::srv::Sound>::SharedPtr srv_;
      };
    }  // namespace devices
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__DEVICES__SOUND_HPP_
